using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Contract.SharedFeatures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.Hubs.Chat
{
    public class MessageChatHub : BaseChatHub
    {
        public IMessageFeatures _messageFeatures;
        public MessageChatHub(IConnectionManager connectionManager, IMessageFeatures messageFeatures) : base(connectionManager)
        {
            _messageFeatures = messageFeatures;
        }

        public async Task SendMessage(int recipientId, string content)
        {
            //var senderId = GetUserId() ?? throw new UnauthorizedAccessException();

            //var message = await _messageFeatures.SendMessageAsync();

            //// Deliver to recipient
            //await Clients.Group($"user-{recipientId}")
            //    .ReceiveMessage(message);
        }
    }
}
