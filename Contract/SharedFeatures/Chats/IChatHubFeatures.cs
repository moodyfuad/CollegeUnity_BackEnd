using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IChatHubFeatures
    {
        Task SendMessageToStaff(int senderId, int receiverId, string message);
        Task SendMessageToStudent(int senderId, int receiverId, string message);
        Task SendMessageToCommunity(int communityId, int userId, string username, string message);
    }
}
