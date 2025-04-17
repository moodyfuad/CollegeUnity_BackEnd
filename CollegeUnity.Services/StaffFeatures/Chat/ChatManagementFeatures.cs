using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Chats;
using CollegeUnity.Core.Dtos.ChatDtos.Create;
using CollegeUnity.Core.Dtos.ChatDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.MappingExtensions.ChatExtentions.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Chat
{
    public class ChatManagementFeatures : IChatManagementFeatures
    {
        private readonly IRepositoryManager _repository;

        public ChatManagementFeatures(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<ResultDto> CreateChatRoom(CChatDto dto)
        {
            var staff = await _repository.StaffRepository.ExistsAsync(dto.StaffId);
            var student = await _repository.StudentRepository.ExistsAsync(dto.StaffId);

            if (student == false || staff == false)
            {
                return new(false, "Can't create chat with this user");
            }

            var isExist = await _repository.ChatRepository.GetByConditionsAsync(c => c.User1Id == dto.StaffId && c.User2Id == dto.StudentId);

            if (isExist != null)
            {
                return new(false, "Chat room already exist.");
            }

            var chat = dto.ToChat();
            await _repository.ChatRepository.CreateAsync(chat);

            return new(true, null);
        }

        public async Task<ResultDto> ManageStatusChatRoom(int chatRoomId, int staffId, UChatDto dto)
        {
            var chatRoom = await _repository.ChatRepository.GetByIdAsync(chatRoomId);

            if (chatRoom is null)
            {
                return new(false, "No chat room with this student");
            }

            if (chatRoom.User1Id == staffId)
            {
                return new(false, "Not your chat room");
            }

            chatRoom.IsChattingEnabled = dto.Status;

            await _repository.ChatRepository.Update(chatRoom);
            await _repository.SaveChangesAsync();

            return new(true, null);
        }
    }
}
