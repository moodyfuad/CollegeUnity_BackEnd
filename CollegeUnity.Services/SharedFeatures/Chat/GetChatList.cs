using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Chat
{
    public class GetChatList : IGetChatList
    {
        private readonly IRepositoryManager _repositoryManager;

        public GetChatList(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        //public async Task<PagedList<GChatsList>> IGetChatList.GetChatList(int userId, GetChatParameters parameters, bool isStaff = true)
        //{
        //    PagedList<GChatsList> chatList;
        //    if (isStaff)
        //    {
        //        await _repositoryManager.ChatRepository.GetRangeByConditionsAsync(c => c.User1Id == userId, parameters);
        //    }
        //}
    }
}
