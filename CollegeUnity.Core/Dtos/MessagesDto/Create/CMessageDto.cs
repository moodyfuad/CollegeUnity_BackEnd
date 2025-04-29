using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.MessagesDto.Create
{
    public class CMessageDto
    {
        public int ChatId {  get; set; }
        public int SenderId { get; set; }
        public int RecipientId {  get; set; }
        public string Message { get; set; }

    }
}
