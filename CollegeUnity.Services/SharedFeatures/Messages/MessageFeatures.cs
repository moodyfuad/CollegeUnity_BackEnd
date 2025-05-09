using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Messages;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Create;
using CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Get;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Messages
{
    public class MessageFeatures : IMessageFeatures
    {        
        private readonly IRepositoryManager _repositoryManager;

        public MessageFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<PagedList<GChatMessagesDto>> GetChatMessages(int chatId, int userId, GetChatMessagesParameters parameters)
        {
            Expression<Func<ChatMessage, bool>> condition = c => c.ChatId == chatId;
            var results = await _repositoryManager.ChatMessageRepository.GetRangeByConditionsAsync(condition, parameters);
            results.OrderByDescending(c => c.CreatedAt);
            return results.GetListOfChats(userId);
        }

        public async Task SendMessageAsync(SendMessageDto dto)
        {
            var message = dto.GetChatMessage();
            await _repositoryManager.ChatMessageRepository.CreateAsync(message);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
