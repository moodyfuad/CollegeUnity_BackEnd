using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Chat
{
    public class MessageValidationFeatures : IMessageValidationFeatures
    {
        private readonly IRepositoryManager _repository;

        public MessageValidationFeatures(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<bool> CanStudentSendMessage(int senderId, int recipientId)
        {
            var sender = await _repository.StudentRepository.GetByIdAsync(senderId);
            var recipient = await _repository.StaffRepository.GetByIdAsync(recipientId);

            if (sender == null || recipient == null) return false;

            return true;
        }

        public async Task<bool> CanStaffSendMessage(int senderId, int recipientId)
        {
            var sender = await _repository.StaffRepository.GetByIdAsync(senderId);
            var recipient = await _repository.StudentRepository.GetByIdAsync(recipientId);

            if (sender == null || recipient == null) return false;

            return true;
        }
    }
}
