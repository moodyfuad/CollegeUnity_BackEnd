using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.ChatDtos
{
    public class UserMessageDto
    {
        private string userId;
        private string Message;
        private DateTime DateTime;

        public UserMessageDto(string userId, string message)
        {
            this.userId = userId;
            Message = message;
            DateTime = DateTime.Now;
        }
    }
}
