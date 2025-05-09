using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.ChatMessageExtentions.Get
{
    public static class GetChatMessagesExtention
    {
        private static GChatMessagesDto GetChatMessage(this ChatMessage chatMessage, int senderId)
        {
            return new()
            {
                Content = chatMessage.Content,
                CreatedAt = chatMessage.CreatedAt,
                IsReadByRecipient = chatMessage.Status,
                IsMe = (chatMessage.SenderId == senderId) ? true : false
            };
        }

        public static PagedList<GChatMessagesDto> GetListOfChats(this PagedList<ChatMessage> chatMessages, int senderId)
        {
            List<GChatMessagesDto> results;
            results = chatMessages.Select(c => c.GetChatMessage(senderId)).ToList();

            var pagedList = new PagedList<GChatMessagesDto>
                (
                    items: results,
                    count: results.Count(),
                    pageNumber: chatMessages.CurrentPage,
                    pageSize: chatMessages.PageSize
                );
            return pagedList;
        }
    }
}
