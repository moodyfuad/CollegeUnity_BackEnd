using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Create
{
    public static class CreateChatMessageExtention
    {
        public static ChatMessage GetChatMessage(this CMessageDto dto)
        {
            return new()
            {
                ChatId = dto.ChatId,
                Content = dto.Message,
                SenderId = dto.SenderId,
                Status = Enums.MessageStatus.Sent,
                CreatedAt = DateTime.Now,
                ReadReceipts = false
            };
        }
    }
}
