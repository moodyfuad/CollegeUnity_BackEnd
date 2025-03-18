using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Staffs
{
    public interface IManageStaffFeatures
    {
        Task<bool> CreateStaffAccount(CreateStaffDto staffDto);
        Task<bool> UpdateStaffAccount(int staffId, UStaffDto dto);
        Task<PagedList<GStaffByRoleDto>> GetStaffByFullName(GetStaffParameters parameters);
        Task<PagedList<GStaffDto>> GetStaffByRole(GetStaffParameters parameters);
        Task<PagedList<GStaffByRoleDto>> GetAllStaff(GetStaffParameters parameters);
        Task<bool> ChangeStaffPassword(int staffId, ChangeStaffPasswordDto dto);
        Task<ResultDto> ChangeStaffAccountStatus(int id, ChangeStaffStatusDto dto);
    }
}
