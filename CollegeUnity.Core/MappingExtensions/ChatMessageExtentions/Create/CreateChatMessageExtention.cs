using CollegeUnity.Core.Dtos.MessagesDto.Create;
using CollegeUnity.Core.Dtos.MessagesDto.Send;
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
        public static ChatMessage GetChatMessage(this SendMessageDto dto, int senderId, int recipientId)
        {
            return new()
            {
                ChatId = dto.ChatId,
                Content = dto.Content,
                SenderId = senderId,
                RecipientId = recipientId,
                Status = Enums.MessageStatus.Sent,
                CreatedAt = DateTime.Now,
            };
        }

        public static CommunityMessage GetChatMessage(this SendMessageToCommunityDto dto, int senderId)
        {
            return new()
            {
                CommunityId = dto.CommunityId,
                Message = dto.Content,
                StudentCommunityId = senderId,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
