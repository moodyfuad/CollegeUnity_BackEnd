using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentServices
{
    public partial class StudentService
    {
        private async Task<bool> _CheckResetPasswordCode(string email, string code)
        {
            var student = await _repositoryManager.StudentRepository.GetByEmail(email);
            if (student == null)
            { 
                return false;
            }
            else
            {
                bool notNull = student.VerificationCode == null ? false : true;

                return notNull ? code == student.VerificationCode : false;
            }
        }
    }
}
