using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<int> GetChatRecipientId(int chatId, int senderId);
    }
}
