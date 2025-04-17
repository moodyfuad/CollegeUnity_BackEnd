using CollegeUnity.Contract.SharedFeatures.Chats;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatHubFeatures _chatHubFeatures;

        public ChatHub(IChatHubFeatures chatHubFeatures)
        {
            _chatHubFeatures = chatHubFeatures;
        }

        //public async Task SendMessageToStaff(string userId, string username, string message)
        //{
        //    await _chatHubFeatures.SendMessageToStaff();
        //}
    }
}
