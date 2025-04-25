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

        public async Task<PagedList<GChatsList>> GetListOfChat(int userId, GetChatParameters parameters, bool isStaff = true)
        {
            Expression<Func<CollegeUnity.Core.Entities.Chat, bool>> expression;
            PagedList<CollegeUnity.Core.Entities.Chat> chatList;
            if (isStaff)
            {
                expression = c =>
                    c.IsHiddenForUser1 == parameters.isHidden &&
                    c.IsChattingEnabled == parameters.isEnabled &&
                    c.User1Id == userId &&
                    (string.IsNullOrEmpty(parameters.SearchName) ||
                    (c.User2.FirstName + " " + c.User2.LastName).Contains(parameters.SearchName));
                
                chatList = await _repositoryManager.ChatRepository.GetRangeByConditionsAsync(expression, parameters, i => i.User2, i => i.Messages);
                return chatList.GetListOfChats();
            }
            else
            {
                expression = c =>
                c.IsHiddenForUser2 == parameters.isHidden &&
                c.IsChattingEnabled == parameters.isEnabled &&
                c.User2Id == userId &&
                (string.IsNullOrEmpty(parameters.SearchName) ||
                (c.User1.FirstName + " " + c.User1.LastName).Contains(parameters.SearchName));

                chatList = await _repositoryManager.ChatRepository.GetRangeByConditionsAsync(expression, parameters, i => i.User1, i => i.Messages);
                return chatList.GetListOfChats(false);
            }
        }
    }
}
