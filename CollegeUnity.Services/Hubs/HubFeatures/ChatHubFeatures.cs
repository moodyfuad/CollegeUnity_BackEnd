using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.ChatDtos;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Dtos.MessagesDto.Send;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Create;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get;
using CollegeUnity.Core.MappingExtensions.CommunityMessageExtentions;
using CollegeUnity.Services.Hubs;
using CollegeUnity.Services.SharedFeatures.Chats;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CollegeUnity.Services.Hubs.HubFeatures
{
    public class ChatHubFeatures : IChatHubFeatures
    {
        private readonly IHubContext<BaseChatHub> _hubContext;
        private readonly IConnectionManager _connectionManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IChatListNotificationFeatures _chatListNotificationFeatures;

        public ChatHubFeatures(
            IHubContext<BaseChatHub> hubContext,
            IConnectionManager connectionManager,
            IRepositoryManager repositoryManager,
            IChatListNotificationFeatures chatListNotificationFeatures)
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
            _repositoryManager = repositoryManager;
            _chatListNotificationFeatures = chatListNotificationFeatures;
        }

        private async Task SendCommunityMessage(int senderId, SendMessageToCommunityDto dto)
        {
            int? studentInCommunityId = await _repositoryManager.StudentCommunityRepository.GetStudentIdInCommunity(senderId, dto.CommunityId);

            if (studentInCommunityId is null)
                throw new UnauthorizedAccessException("can not send, student id is required.");

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Message content cannot be empty");

            var communityMessage = CommunityMessageExtention
                .ToCommunityMessage(studentInCommunityId.Value, dto.CommunityId, dto.Content);

            string studentName = await _repositoryManager.StudentRepository.GetFullName(senderId);

            await _hubContext.Clients
                .Group($"community-{dto.CommunityId}")
                .SendAsync("ReceiveCommunityMessage", communityMessage.GetMessage(senderId, studentName));

            await _repositoryManager.CommunityMessagesRepository.CreateAsync(communityMessage);
            await _repositoryManager.SaveChangesAsync();

            await _chatListNotificationFeatures.NotifyNewMessageInCommunity(dto.CommunityId, dto.Content, senderId);
        }

        public async Task SendMessageToUCommunity(int senderId, SendMessageToCommunityDto dto)
        {
            var studentRole = await _repositoryManager.StudentCommunityRepository
                .GetStudentRoleInCommunity(senderId, dto.CommunityId);

            var communityType = await _repositoryManager.CommunityRepository
                .TypeOfCommunity(dto.CommunityId);

            bool isLocked = communityType == CommunityType.LookedPrivate || communityType == CommunityType.Private;
            bool isAdmin = studentRole == CommunityMemberRoles.Admin || studentRole == CommunityMemberRoles.SuperAdmin;

            if (!isLocked || (isLocked && isAdmin))
            {
                await SendCommunityMessage(senderId, dto);
            }
            else
            {
                throw new UnauthorizedAccessException("You are not allowed to send messages in this community.");
            }
        }


        public async Task SendMessageToUser(int senderId, SendMessageDto dto)
        {
            int recipientId = await _repositoryManager.ChatRepository.GetChatRecipientId(dto.ChatId, senderId);

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Message content cannot be empty");

            var sendMessage = dto.GetChatMessage(senderId, recipientId);

            var recipientConnectionId = _connectionManager.GetConnection(recipientId);
            var isRecipientInChatRoom = _connectionManager.IsUserInChatRoom(recipientId, dto.ChatId);
            var senderConnectionId = _connectionManager.GetConnection(senderId);

            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                sendMessage.Status = Core.Enums.MessageStatus.Delivered;
            }

            if (isRecipientInChatRoom)
            {
                sendMessage.Status = Core.Enums.MessageStatus.Read;
            }

            Task send = _hubContext.Clients
                .Group($"chat-{dto.ChatId}")
                .SendAsync("ReceiveMessage", sendMessage);

            await _repositoryManager.ChatMessageRepository.CreateAsync(sendMessage);
            await _repositoryManager.SaveChangesAsync();

            var uncreadCounter = await _repositoryManager.ChatMessageRepository.GetUnreadMessages(dto.ChatId, recipientId);

            GChatsList getChat = new()
            {
                ChatRoomId = dto.ChatId,
                IsNew = true,
                LastMessageSent = dto.Content,
                UnreadCounter = uncreadCounter,
                Time = sendMessage.CreatedAt,
                Sender = "",
                PicturePath = "",
            };

            Task notify = _chatListNotificationFeatures.NotifyNewChat(recipientId, getChat);

            await Task.WhenAll(send, notify);
        }
    }
}
