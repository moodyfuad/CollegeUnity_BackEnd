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
        public static GChatsList GetChat(this Chat chat, bool isStaff = true)
        {
            // Get last message once to avoid multiple queries
            var lastMessage = chat.Messages?
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault();

            return new()
            {
                ChatRoomId = chat.Id,
                EnableSending = chat.IsChattingEnabled,
                Sender = isStaff ? chat.User2.FirstName + " " + chat.User2.LastName : chat.User1.FirstName + " " + chat.User1.LastName,
                LastMessageSent = lastMessage?.Content.TruncateContent(100),
                PicturePath = isStaff ? chat.User2.ProfilePicturePath : chat.User1.ProfilePicturePath,
                UnreadCounter = chat.Messages?
                    .Count(m => m.Status == MessageStatus.Sent &&
                               m.SenderId != lastMessage?.SenderId // Use the lastMessage we already fetched
                               ) ?? 0, // Proper collection check
                Time = lastMessage?.CreatedAt ?? DateTime.UtcNow // Fallback to current time if no messages
            };
        }

        private static string TruncateContent(this string content, int maxLength = 100)
        {
            if (string.IsNullOrEmpty(content)) return content;
            return content.Length <= maxLength ? content : $"{content[..maxLength]}...";
        }

        public static PagedList<GChatsList> GetListOfChats(this PagedList<Chat> chats, bool isStaff = true)
        {
            List<GChatsList> results;
            if (isStaff)
            {
                results = chats.Select(c => c.GetChat(true)).ToList();
            }
            else
            {
                results = chats.Select(c => c.GetChat(false)).ToList();
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
