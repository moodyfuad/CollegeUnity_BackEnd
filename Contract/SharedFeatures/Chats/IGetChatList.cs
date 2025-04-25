using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IGetChatList
    {
        public Task<PagedList<GChatsList>> GetListOfChat(int userId, GetChatParameters parameters, bool isStaff = true);
    }
}
