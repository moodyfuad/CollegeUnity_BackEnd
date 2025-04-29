using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.Hubs
{
    public class BaseChatHub : Hub
    {
        private readonly IConnectionManager _connectionManager;

        public BaseChatHub(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = Context.User?.FindFirst(CustomClaimTypes.Id);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.AddConnection(userId.Value, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserIdFromClaims();
            if (userId != null)
            {
                _connectionManager.RemoveConnection(userId.Value);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
