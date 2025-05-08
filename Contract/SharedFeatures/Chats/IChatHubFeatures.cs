using CollegeUnity.Core.Dtos.MessagesDto.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IChatHubFeatures
    {
        Task SendMessageToUser(SendMessageDto dto);
    }
}
