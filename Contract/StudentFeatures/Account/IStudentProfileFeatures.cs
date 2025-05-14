using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.StudentFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Account
{
    public interface IStudentProfileFeatures
    {
        Task<ApiResponse<GetStudentProfileDto?>> GetInfo(int studentId);

        Task<ApiResponse<bool>> Update(int studentId, UpdateUserProfileDto dto);

        Task<ApiResponse<bool>> UpdatePassword(int studentId, UpdateUserPasswordDto dto);
    }
}
