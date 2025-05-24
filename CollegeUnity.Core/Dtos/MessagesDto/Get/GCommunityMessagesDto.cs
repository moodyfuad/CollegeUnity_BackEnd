using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.MessagesDto.Get
{
    public class GCommunityMessagesDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
