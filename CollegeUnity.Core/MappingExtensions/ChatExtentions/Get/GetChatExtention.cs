using CollegeUnity.Core.Dtos.ChatDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.ChatExtentions.Get
{
    public static class GetChatExtention
    {
        public static GChatsList GetChat(this Chat chat, int userId, bool isStaff = true)
        {
            // Get last message once to avoid multiple queries
            var lastMessage = chat.Messages?
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault();

            return new()
            {
                ChatRoomId = chat.Id,
                Sender = isStaff ? chat.User2.FirstName + " " + chat.User2.LastName : chat.User1.FirstName + " " + chat.User1.LastName,
                LastMessageSent = lastMessage?.Content,
                PicturePath = isStaff ? chat.User2.ProfilePicturePath : chat.User1.ProfilePicturePath,
                UnreadCounter = chat.Messages?
                    .Count(m => m.Status == MessageStatus.Sent &&
                               m.SenderId != userId
                               ) ?? 0, 
                Time = lastMessage?.CreatedAt ?? DateTime.UtcNow
            };
        }

        public static PagedList<GChatsList> GetListOfChats(this PagedList<Chat> chats, int userId, bool isStaff = true)
        {
            List<GChatsList> results;
            if (isStaff)
            {
                results = chats.Select(c => c.GetChat(userId, true)).ToList();
            }
            else
            {
                results = chats.Select(c => c.GetChat(userId, false)).ToList();
            }
                var pagedList = new PagedList<GChatsList>
                    (
                        items: results,
                        count: results.Count(),
                        pageNumber: chats.CurrentPage,
                        pageSize: chats.PageSize
                    );
            return pagedList;
        }
    }
}
