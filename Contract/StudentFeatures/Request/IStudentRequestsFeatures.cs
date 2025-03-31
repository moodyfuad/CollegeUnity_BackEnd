using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Request
{
    public interface IStudentRequestsFeatures
    {
        Task<ApiResponse<string?>> Send(int studentId, int staffId, SendRequestDto sendRequestDto);

        Task<ApiResponse<PagedList<GetUserRequestsDto>>> Get(int studentId, GetStudentRequestsQueryString queryString);
    }
}
