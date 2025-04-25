using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.ChatDtos.Get
{
    public class GChatsList
    {
        public int ChatRoomId { get; set; }
        public string Sender { get; set; }
        public string? LastMessageSent { get; set; }
        public DateTime? Time { get; set; }
        public int UnreadCounter { get; set; }
        public bool EnableSending { get; set; }
    }
}
