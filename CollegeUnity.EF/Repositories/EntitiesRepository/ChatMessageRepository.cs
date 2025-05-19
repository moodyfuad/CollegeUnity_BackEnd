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
                i => i.RecipientId == recipientId &&
                     i.ChatId == chatId &&
                      i.Status == Core.Enums.MessageStatus.Delivered
            );
        }

        public async Task MakeMessagesReadAsync(int chatId, int userId)
        {
            var messagesToUpdate = await _context.ChatMessages
                .Where(m =>
                    m.SenderId != userId &&
                    m.ChatId == chatId &&
                     m.Status == Core.Enums.MessageStatus.Delivered)
                .ToListAsync();

            foreach (var message in messagesToUpdate)
            {
                message.Status = Core.Enums.MessageStatus.Read;
            }
            await _context.SaveChangesAsync();
        }

        public async Task MakrUserMessagesAsDeliverd(int userId)
        {
            var messagesToUpdate = await _context.ChatMessages
                .Where(m =>
                    m.SenderId != userId &&
                    m.Status == Core.Enums.MessageStatus.Sent)
                .ToListAsync();

            foreach (var message in messagesToUpdate)
            {
                message.Status = Core.Enums.MessageStatus.Delivered;
            }
            await _context.SaveChangesAsync();
        }

    }
}
