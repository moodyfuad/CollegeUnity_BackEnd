using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Dtos.MessagesDto.Send;
using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.Hubs
{
    public class BaseChatHub : Hub
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IChatHubFeatures _chatHubFeatures;
        private readonly IRepositoryManager _repositoryManager;

        public BaseChatHub(IConnectionManager connectionManager, IChatHubFeatures chatHubFeatures, IRepositoryManager repositoryManager)
        {
            _connectionManager = connectionManager;
            _chatHubFeatures = chatHubFeatures;
            _repositoryManager = repositoryManager;
        }

        public int? GetUserIdFromClaims()
        {
            var userIdClaim = Context.User?.FindFirst(CustomClaimTypes.Id);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                await _repositoryManager.ChatMessageRepository.MakrUserMessagesAsDeliverd(userId.Value);
                _connectionManager.AddConnection(userId.Value, Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId.Value}");
            }
            await base.OnConnectedAsync();
        }

        public async Task joinCommunityRoom(int communityId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"community-{communityId}");
        }

        public async Task LeaveCommunityRoom(int communityId)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"community-{communityId}");
                await _repositoryManager.StudentCommunityRepository.SetMyLastSeen(userId.Value, communityId);
                await _repositoryManager.SaveChangesAsync();
            }
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.RemoveConnection(userId.Value);
                _connectionManager.RemoveUserFromChatRoom(userId.Value);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user-{userId.Value}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(SendMessageDto dto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                throw new UnauthorizedAccessException("Student not logged in.");


            await _chatHubFeatures.SendMessageToUser(userId.Value, dto);
        }

        public async Task SendMessageToCommunity(SendMessageToCommunityDto dto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                throw new UnauthorizedAccessException("Student not logged in.");


            await _chatHubFeatures.SendMessageToUCommunity(userId.Value, dto);
        }

        public Task JoinChatRoom(int chatId)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.SetUserCurrentChatRoom(userId.Value, chatId);
                return Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{chatId}");
            }
            throw new UnauthorizedAccessException("Student not logged in.");
        }

        public Task LeaveChatRoom(int chatId)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.RemoveUserFromChatRoom(userId.Value);
                return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"chat-{chatId}");
            }
            throw new UnauthorizedAccessException("Student not logged in.");
        }
    }
}
