using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Dtos.StudentFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Request
{
    public interface IRequestsFeature
    {
        Task<ApiResponse<string?>> Send(int studentId, int staffId, SendRequestDto sendRequestDto);

        Task<ApiResponse<ICollection<GetStudentRequestsDto>>> Get(int studentId);
    }
}
