using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IStudentServices
    {
        Task<PagedList<Student>> GetStudentsAsync(StudentSearchParameters searchParameters);
            
    }
}
