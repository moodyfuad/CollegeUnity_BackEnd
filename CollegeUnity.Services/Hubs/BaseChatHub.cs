using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.MessagesDto.Create;
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
                throw new UnauthorizedAccessException("User not logged in.");


            await _chatHubFeatures.SendMessageToUser(userId.Value, dto);
        }

        public Task JoinChatRoom(int chatId)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.SetUserCurrentChatRoom(userId.Value, chatId);
                return Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{chatId}");
            }
            throw new UnauthorizedAccessException("User not logged in.");
        }

        public Task LeaveChatRoom(int chatId)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.RemoveUserFromChatRoom(userId.Value);
                return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"chat-{chatId}");
            }
            throw new UnauthorizedAccessException("User not logged in.");
        }
    }
}
