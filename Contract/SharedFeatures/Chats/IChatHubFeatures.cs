using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Dtos.MessagesDto.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IChatHubFeatures
    {
        Task SendMessageToUser(int senderId, SendMessageDto dto);
        Task SendMessageToUCommunity(int senderId, SendMessageToCommunityDto dto);
    }
}
