using CollegeUnity.Contract.SharedFeatures.Chats;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.Hubs.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly ConcurrentDictionary<int, string> _connections = new();

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
    }
}
