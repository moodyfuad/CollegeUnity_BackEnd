using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using CollegeUnity.Core.Dtos.FeedBackDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.FeedBacks
{
    public interface IManageFeedBackFeatures
    {
        public Task<PagedList<GFeedBackDto>> GetFeedBacks(GetFeedBackParameters parameters);
        public Task<ResultDto> FinalizeFeedback(int feedbackId, UFeedBackResponseDto dto);
    }
}
