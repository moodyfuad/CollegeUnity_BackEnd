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
        public static GChatsList GetChat(this Chat chat, int userId)
        {
            var lastMessage = chat.Messages?
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault();

            var isUser1 = chat.User1Id == userId;
            var otherUser = isUser1 ? chat.User2 : chat.User1;

            return new GChatsList()
            {
                ChatRoomId = chat.Id,
                Sender = otherUser.FirstName + " " + otherUser.LastName,
                LastMessageSent = lastMessage?.Content,
                PicturePath = otherUser.ProfilePicturePath,
                UnreadCounter = chat.Messages?
                    .Count(m => m.Status == MessageStatus.Sent &&
                               m.SenderId != userId) ?? 0,
                Time = lastMessage?.CreatedAt ?? DateTime.UtcNow
            };
        }

        public static PagedList<GChatsList> GetListOfChatsWithOtherUserNames(this PagedList<Chat> chats, int userId)
        {
            var results = chats.Select(c => c.GetChat(userId)).ToList();

            return new PagedList<GChatsList>
                (
                    items: results,
                    count: results.Count,
                    pageNumber: chats.CurrentPage,
                    pageSize: chats.PageSize
                );
        }
    }
}
