using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Messages;
using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Messages
{
    public class MessageFeatures : IMessageFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public async Task SendMessageAsync(CMessageDto dto)
        {
            var message = dto.GetChatMessage();
            await _repositoryManager.ChatMessageRepository.CreateAsync(message);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
