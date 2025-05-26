using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Feedbacks
{
    public interface ISendFeedBackFeatures
    {
        Task<ResultDto> SendFeedBack(int userId, CFeedBackResponseDto dto);
    }
}
