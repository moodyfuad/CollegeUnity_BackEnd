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

namespace CollegeUnity.Services.Hubs.HubFeatures
{
    public class ChatHubFeatures : IChatHubFeatures
    {
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

        public Task SendMessageToUser(int chatId, int senderId, int receiverId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
