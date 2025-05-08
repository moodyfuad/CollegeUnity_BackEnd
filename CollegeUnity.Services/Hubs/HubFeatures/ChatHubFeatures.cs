using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.ChatDtos;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Create;
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

        //private readonly IHubContext hub;
        //public Task SendMessageToUser(int chatId, int senderId, string message)
        //{
        //    var cahtMessage = new ChatMessage
        //    {
        //        ChatId = chatId,
        //        Content = message,
        //        CreatedAt = DateTime.UtcNow,
        //        ReadReceipts = false,
        //        Status = Core.Enums.MessageStatus.Sent,
        //        SenderId = senderId,
        //    };
        //}

        public async Task SendMessageToUser(SendMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Message content cannot be empty");

            var sendMessage = dto.GetChatMessage();

            var recipientConnectionId = _connectionManager.GetConnection(dto.RecipientId);

            if (!string.IsNullOrEmpty(recipientConnectionId))
                sendMessage.Status = Core.Enums.MessageStatus.Delivered;

            await _repositoryManager.ChatMessageRepository.CreateAsync(sendMessage);
            await _repositoryManager.SaveChangesAsync();

            var groupTask = _hubContext.Clients
                    .Group($"user-{dto.RecipientId}")
                    .SendAsync("ReceiveMessage", sendMessage);

            var directTask = !string.IsNullOrEmpty(recipientConnectionId) ? _hubContext.Clients
                    .Client(recipientConnectionId)
                    .SendAsync("ReceiveMessage", sendMessage) : Task.CompletedTask;

            GChatsList getChat = new()
            {
                ChatRoomId = dto.ChatId,
                IsNew = true,
                LastMessageSent = dto.Content,
                UnreadCounter = await _repositoryManager.ChatMessageRepository.GetUnreadMessages(dto.ChatId, dto.RecipientId),
                Time = sendMessage.CreatedAt,
                Sender = "",
                PicturePath = "",
            };

            var newMessageAtChat = _chatListNotificationFeatures.NotifyNewChat(dto.RecipientId, getChat);


            await Task.WhenAll(groupTask, directTask, newMessageAtChat);
        }
    }
}
