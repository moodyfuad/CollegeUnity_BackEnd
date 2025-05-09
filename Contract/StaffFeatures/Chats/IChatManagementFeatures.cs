using CollegeUnity.Core.Dtos.ChatDtos.Create;
using CollegeUnity.Core.Dtos.ChatDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Chats
{
    public interface IChatManagementFeatures
    {
        Task<ResultDto> CreateChatRoom(int staffId, int studentId);
        Task<ResultDto> ManageStatusChatRoom(int chatRoomId, int staffId, UChatDto dto);
    }
}
