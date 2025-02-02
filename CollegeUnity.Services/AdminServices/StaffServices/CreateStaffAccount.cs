using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminServices
{
    public partial class AdminService
    {
        private static string GetExistedProperty(IEnumerable<Staff> staff,CreateStaffDto dto)
        {
            return staff.FirstOrDefault(s => s.Email == dto.Email).Email ??
             staff.FirstOrDefault(s => s.Phone == dto.Phone).Phone;
        }
    }
}
