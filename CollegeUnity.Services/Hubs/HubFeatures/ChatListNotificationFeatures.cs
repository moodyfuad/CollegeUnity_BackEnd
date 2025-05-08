using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CollegeUnity.Services.Hubs.HubFeatures
{
    public class ChatListNotificationFeatures : IChatListNotificationFeatures
    {
        private readonly IHubContext<BaseChatHub> _hubContext;
        private readonly IConnectionManager _connectionManager;

        public ChatListNotificationFeatures(
            IHubContext<BaseChatHub> hubContext,
            IConnectionManager connectionManager)
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
        }

        public async Task NotifyNewChat(int recipientUserId, GChatsList chat)
        {
            chat.IsNew = true;
            // 1. PRIMARY: Group notification (all devices)
            var groupTask = _hubContext.Clients
                .Group($"user-{recipientUserId}")
                .SendAsync("ReceiveNewChat", chat);

            // 2. FALLBACK: Direct connection (single device)
            var connectionId = _connectionManager.GetConnection(recipientUserId);
            var directTask = connectionId != null
                ? _hubContext.Clients
                    .Client(connectionId)
                    .SendAsync("ReceiveNewChat", chat)

                : Task.CompletedTask;

            await Task.WhenAll(groupTask, directTask);
        }
    }
}
