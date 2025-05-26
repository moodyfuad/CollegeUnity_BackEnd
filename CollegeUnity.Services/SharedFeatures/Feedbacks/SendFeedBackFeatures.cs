using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Feedbacks;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using CollegeUnity.Core.MappingExtensions.FeedBackExtensions.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Feedbacks
{
    public class SendFeedBackFeatures : ISendFeedBackFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public SendFeedBackFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ResultDto> SendFeedBack(int userId, CFeedBackResponseDto dto)
        {
            var user = await _repositoryManager.UserRepository.GetByIdAsync(userId);
            if (user == null)
                return new(false, "User not found!");

            var feedback = dto.ToFeedBack(userId);
            await _repositoryManager.FeedBackRepository.CreateAsync(feedback);
            await _repositoryManager.SaveChangesAsync();
            return new(true);
        }
    }
}
