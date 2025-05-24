using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get
{
    public static class GetCommunityMessageExtention
    {
        public static GCommunityMessagesDto GetMessage(this CommunityMessage message)
        {
            return new()
            {
                Id = message.Id,
                SenderId = message.StudentCommunity.StudentId,
                DateTime = message.CreatedAt,
                Message = message.Message,
                SenderName = string.Concat(message.StudentCommunity.Student.FirstName + " " + message.StudentCommunity.Student.MiddleName + " " + message.StudentCommunity.Student.LastName)
            };
        }

        public static GCommunityMessagesDto GetMessage(this CommunityMessage message, int senderId, string studentName)
        {
            return new()
            {
                Id = message.Id,
                SenderId = senderId,
                DateTime = message.CreatedAt,
                Message = message.Message,
                SenderName = studentName
            };
        }
    }
}
