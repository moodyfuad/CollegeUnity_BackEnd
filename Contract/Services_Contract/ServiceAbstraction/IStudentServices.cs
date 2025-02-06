using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using EmailService.Models;
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
        //Task<Result> SendResetPasswordRequest(string email);
        Task<bool> CheckResetPasswordCode(string email, string code);
        Task<bool> ResetPassword(string email, string code, string newPassword);
            
    }
}
