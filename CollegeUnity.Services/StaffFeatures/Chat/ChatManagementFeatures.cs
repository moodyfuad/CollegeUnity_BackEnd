using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Chats;
using CollegeUnity.Core.Dtos.ChatDtos.Create;
using CollegeUnity.Core.Dtos.ChatDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.MappingExtensions.ChatExtentions.Create;
using CollegeUnity.Core.MappingExtensions.ChatExtentions.Get;
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

        public async Task<ResultDto> CreateChatRoom(int userId1, int userId2)
        {
            var user1Exists = await _repository.UserRepository.ExistsAsync(userId1);
            var user2Exists = await _repository.UserRepository.ExistsAsync(userId2);

            if (!user1Exists || !user2Exists)
            {
                return new(false, "Can't create chat with this user");
            }

            var isExist = await _repository.ChatRepository.GetByConditionsAsync(c =>
                (c.User1Id == userId1 && c.User2Id == userId2) ||
                (c.User1Id == userId2 && c.User2Id == userId1)
            );

            if (isExist != null)
            {
                return new(false, "Chat room already exists.");
            }

            var chat = new Core.Entities.Chat()
            {
                User1Id = userId1,
                User2Id = userId2,
                IsChattingEnabled = true,
                CreateAt = DateTime.Now,
            };

            await _repository.ChatRepository.CreateAsync(chat);
            await _repository.SaveChangesAsync();

            var data = chat.GetChat(userId2);

            return new(true, null, data, userId2);
        }



        public async Task<ResultDto> ManageStatusChatRoom(int chatRoomId, int staffId, UChatDto dto)
        {
            var chatRoom = await _repository.ChatRepository.GetByIdAsync(chatRoomId);

            if (chatRoom is null)
            {
                return new(false, "No chat room with this id");
            }

            var isUser1Staff = await _repository.StaffRepository.ExistsAsync(chatRoom.User1Id);
            var isUser2Staff = await _repository.StaffRepository.ExistsAsync(chatRoom.User2Id);

            if (isUser1Staff && isUser2Staff)
            {
                return new(false, "Cannot modify chat status when both users are staff.");
            }

            if (chatRoom.User1Id != staffId && chatRoom.User2Id != staffId)
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
