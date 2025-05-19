using CollegeUnity.Contract.EF_Contract;
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
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        private readonly CollegeUnityDbContext _context;
        public ChatRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetChatRecipientId(int chatId, int senderId)
        {
            var chat = await _context.Chats
                .Where(c => c.Id == chatId)
                .Select(c => new { c.User1Id, c.User2Id })
                .FirstOrDefaultAsync();

            if (chat.User1Id == senderId)
                return chat.User2Id;
            else
                return chat.User1Id;
        }

    }
}
