using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
    {
        private readonly CollegeUnityDbContext _context;
        public ChatMessageRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetUnreadMessages(int chatId, int recipientId)
        {
            return await _context.ChatMessages.CountAsync(
                i => i.SenderId != recipientId &&
                     i.ChatId == chatId &&
                     (i.Status == Core.Enums.MessageStatus.Sent ||
                      i.Status == Core.Enums.MessageStatus.Delivered)
            );
        }
    }
}
