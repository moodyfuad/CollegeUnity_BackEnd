using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Chats
{
    public interface IMessageValidationFeatures
    {
        Task<bool> CanStudentSendMessage(int senderId, int recipientId);
        Task<bool> CanStaffSendMessage(int senderId, int recipientId);
    }
}
