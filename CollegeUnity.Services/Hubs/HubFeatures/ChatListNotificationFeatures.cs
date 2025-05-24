using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Entities;
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
        private readonly IRepositoryManager _repositoryManager;

        public ChatListNotificationFeatures(
            IHubContext<BaseChatHub> hubContext,
            IConnectionManager connectionManager,
            IRepositoryManager repositoryManager
            )
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
            _repositoryManager = repositoryManager;
        }

        public async Task NotifyNewChat(int recipientUserId, GChatsList chat)
        {
            chat.IsNew = true;
            // 2. FALLBACK: Direct connection (single device)
            var connectionId = _connectionManager.GetConnection(recipientUserId);
            if (connectionId != null)
            {
                await _hubContext.Clients
                    .Client(connectionId)
                    .SendAsync("ReceiveNewChat", chat);
            }
        }

        public async Task NotifyNewMessageInCommunity(int communityId, string content, int excludedStudentId)
        {
            var studentIds = await _repositoryManager.StudentCommunityRepository.GetStudentIdsInCommunity(communityId);

            var tasks = new List<Task>();

            foreach (var studentId in studentIds)
            {

                tasks.Add(Task.Run(async () =>
                {
                    var lastMessagesCounter = await _repositoryManager.StudentCommunityRepository
                        .GetUnreadMessagesFromLastSeen(studentId, communityId);

                    GStudentCommunitesDto dto = new()
                    {
                        Id = communityId,
                        UnreadCounter = lastMessagesCounter,
                        LastMessage = content
                    };

                    var connectionId = _connectionManager.GetConnection(studentId);
                    if (connectionId != null)
                    {
                        await _hubContext.Clients
                            .Client(connectionId)
                            .SendAsync("ReceiveNewCommunityMessage", dto);
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }


    }
}
