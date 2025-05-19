using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IConnectionManager
    {
        void AddConnection(int userId, string connectionId);
        void RemoveConnection(int userId);
        string GetConnection(int userId);
        void SetUserCurrentChatRoom(int userId, int chatRoomId);
        void RemoveUserFromChatRoom(int userId);
        bool IsUserInChatRoom(int userId, int chatRoomId);
    }
}
