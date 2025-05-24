using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.MappingExtensions.ChatExtentions.Get;

namespace CollegeUnity.Services.SharedFeatures.Chats
{
    public class GetChatList : IGetChatList
    {
        private readonly IRepositoryManager _repositoryManager;

        public GetChatList(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        //This is between staff and student
        public async Task<PagedList<GChatsList>> GetListOfChat(int userId, GetChatParameters parameters)
        {
            Expression<Func<CollegeUnity.Core.Entities.Chat, bool>> expression = c =>
                ((c.User1Id == userId && c.IsHiddenForUser1 == parameters.isHidden) ||
                  (c.User2Id == userId && c.IsHiddenForUser2 == parameters.isHidden))
                && c.IsChattingEnabled == parameters.isEnabled
                && (string.IsNullOrEmpty(parameters.SearchName) ||
                    (
                        (c.User1Id == userId && (c.User2.FirstName + " " + c.User2.LastName).Contains(parameters.SearchName)) ||
                        (c.User2Id == userId && (c.User1.FirstName + " " + c.User1.LastName).Contains(parameters.SearchName))
                    ));

            var chatList = await _repositoryManager.ChatRepository.GetRangeByConditionsAsync(
                expression,
                parameters,
                i => i.User1,
                i => i.User2,
                i => i.Messages);

            return chatList.GetListOfChatsWithOtherUserNames(userId);
        }
    }
}
