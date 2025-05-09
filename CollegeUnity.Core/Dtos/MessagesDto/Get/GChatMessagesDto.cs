using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.MessagesDto.Get
{
    public class GChatMessagesDto
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public MessageStatus IsReadByRecipient { get; set; }
        public bool IsMe { get; set; }
    }
}
