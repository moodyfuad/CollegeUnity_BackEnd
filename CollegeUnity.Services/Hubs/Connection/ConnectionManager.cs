using CollegeUnity.Contract.SharedFeatures.Chats;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CollegeUnity.Services.Hubs.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly ConcurrentDictionary<int, string> _connections = new();
        private readonly ConcurrentDictionary<int, int> _userCurrentChatRoom = new();

        public void AddConnection(int userId, string connectionId)
        {
            _connections.AddOrUpdate(userId, connectionId, (_, _) => connectionId);
        }

        public void RemoveConnection(int userId)
        {
            _connections.TryRemove(userId, out _);
        }

        public string GetConnection(int userId)
        {
            return _connections.TryGetValue(userId, out var connectionId) ? connectionId : null;
        }

        public void SetUserCurrentChatRoom(int userId, int chatRoomId)
        {
            _userCurrentChatRoom.AddOrUpdate(userId, chatRoomId, (_, _) => chatRoomId);
        }

        public void RemoveUserFromChatRoom(int userId)
        {
            _userCurrentChatRoom.TryRemove(userId, out _);
        }

        public bool IsUserInChatRoom(int userId, int chatRoomId)
        {
            return _userCurrentChatRoom.TryGetValue(userId, out int currentChatId) && currentChatId == chatRoomId;
        }
    }
}
