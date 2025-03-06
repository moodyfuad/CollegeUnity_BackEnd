using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
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
    }
}
