using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IChatMessageRepository : IBaseRepository<ChatMessage>
    {
        Task<int> GetUnreadMessages(int chatId, int recipientId);
        Task MakeMessagesReadAsync(int chatId, int userId);
        Task MakrUserMessagesAsDeliverd(int userId);
    }
}
