using CollegeUnity.Contract.AdminFeatures.FeedBacks;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using CollegeUnity.Core.Dtos.FeedBackDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.FeedBackExtensions.Create;
using CollegeUnity.Core.MappingExtensions.FeedBackExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.FeedBacks
{
    public class ManageFeedBackFeatures : IManageFeedBackFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public ManageFeedBackFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GFeedBackDto>> GetFeedBacks(GetFeedBackParameters parameters)
        {
            var feedbacks = await _repositoryManager.FeedBackRepository.GetRangeByConditionsAsync(
            i => i.FeedBackStatus == parameters.enFeedBackStatus, parameters, i => i.FromUser);
            return feedbacks.GetFeedBacks();
        }

        public async Task<ResultDto> FinalizeFeedback(int feedbackId, UFeedBackResponseDto dto)
        {
            var feedback = await _repositoryManager.FeedBackRepository.GetByConditionsAsync(i => i.Id == feedbackId);

            if (feedback == null)
            {
                return new(false, "No Feedback found.");
            }

            feedback.Response = dto.Response;
            feedback.FeedBackStatus = dto.Status;

            await _repositoryManager.FeedBackRepository.Update(feedback);
            await _repositoryManager.SaveChangesAsync();
            
            return new(true);
        }
    }
}
