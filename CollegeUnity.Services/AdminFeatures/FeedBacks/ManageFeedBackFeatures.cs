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
            var feedbacks = await _repositoryManager.FeedBackRepository.GetRangeAsync(parameters, i => new
            {
                i.FromUser.Id,
                i.FromUser.FirstName,
                i.FromUser.LastName,
            });
            return feedbacks.GetFeedBacks();
        }

        public async Task<ResultDto> FinalizeFeedback(int feedbackId, UFeedBackResponseDto dto)
        {
            var feedback = await _repositoryManager.FeedBackRepository.GetByConditionsAsync(i => i.Id == feedbackId);

            if (feedback == null)
            {
                return new(false, "No Feedback found.");
            }

            var updatedFeedBack = feedback.ToFeedBack(dto);
            await _repositoryManager.FeedBackRepository.Update(updatedFeedBack);
            await _repositoryManager.SaveChangesAsync();
            
            return new(true);
        }
    }
}
