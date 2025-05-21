using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IChatListNotificationFeatures
    {
        Task NotifyNewChat(int recipientUserId, GChatsList chat);
        Task NotifyNewMessageInCommunity(int communityId, string content);
    }
}
