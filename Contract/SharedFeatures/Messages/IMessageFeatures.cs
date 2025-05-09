using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Messages
{
    public interface IMessageFeatures
    {
        public Task SendMessageAsync(SendMessageDto dto);
        public Task<PagedList<GChatMessagesDto>> GetChatMessages(int chatId, int userId, GetChatMessagesParameters parameters);
    }
}
