using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
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
        Task<IEnumerable<GStaffByRoleDto>> GetStaffByFullName(GetStaffParameters parameters);
        Task<IEnumerable<GStaffDto>> GetStaffByRole(GetStaffParameters parameters);
        Task<IEnumerable<GStaffByRoleDto>> GetAllStaff(GetStaffParameters parameters);
        Task<bool> ChangeStaffPassword(int staffId, ChangeStaffPasswordDto dto);
    }
}
