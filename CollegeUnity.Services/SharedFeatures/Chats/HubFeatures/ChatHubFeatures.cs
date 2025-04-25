using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Dtos.ChatDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Chat.Hub
{
    public class ChatHubFeatures : IChatHubFeatures
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IRepositoryManager _repository;
        private readonly IMessageValidationFeatures _validationFeatures;

        public ChatHubFeatures
            (IHubContext<ChatHub> hubContext,
            IRepositoryManager repository,
            IMessageValidationFeatures validationFeatures)
        {
            _hubContext = hubContext;
            _repository = repository;
            _validationFeatures = validationFeatures;
        }

        public async Task SendMessageToStaff(int senderId, int receiverId, string message)
        {
            var isSuccess = await _validationFeatures.CanStudentSendMessage(senderId, receiverId);

            if (isSuccess)
            {
                var chatMessage = new UserMessageDto(senderId.ToString(), message);
                await _hubContext.Clients.User(receiverId.ToString()).SendAsync("SendMessageToStaff", chatMessage);
                //_repository.ChatMessageRepository.CreateAsync();
            }
        }

        public Task SendMessageToStudent(int senderId, int receiverId, string message)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageToCommunity(int communityId, int userId, string username, string message)
        {
            throw new NotImplementedException();
        }
    }
}
