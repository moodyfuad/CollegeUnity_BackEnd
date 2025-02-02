using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IAdminServices
    {
        Task<ApiResponse<CreateStaffDto>> CreateStaffAccount(CreateStaffDto staffDto);

        Task<ApiResponse<IEnumerable<Staff>>> SearchStaffBy(Expression<Func<Staff,bool>> expression);
        Task<ApiResponse<IEnumerable<Staff>>> SearchStaffBy(string name);
        Task<ApiResponse<IEnumerable<CreateStaffDto>>> GetAllStaff();
        Task<ApiResponse<IEnumerable<Student>>> SearchStudentsBy(string name);

    }
}
