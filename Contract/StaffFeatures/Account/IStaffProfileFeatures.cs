using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.StaffFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Account
{
    public interface IStaffProfileFeatures
    {
        Task<ApiResponse<GetStaffProfileDto?>> GetInfo(int staffId);

        Task<ApiResponse<bool>> Update(int staffId, UpdateUserProfileDto dto);

        Task<ApiResponse<bool>> UpdatePassword(int staffId, UpdateUserPasswordDto dto);
    }
}
