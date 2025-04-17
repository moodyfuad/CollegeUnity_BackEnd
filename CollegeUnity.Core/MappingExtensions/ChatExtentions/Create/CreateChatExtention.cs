using CollegeUnity.Core.Dtos.ChatDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.ChatExtentions.Create
{
    public static class CreateChatExtention
    {
        public static Chat ToChat(this CChatDto dto)
        {
            return new()
            {
                User1Id = dto.StaffId,
                User2Id = dto.StudentId,
                IsChattingEnabled = true,
                CreateAt = DateTime.Now
            };
        }
    }
}
