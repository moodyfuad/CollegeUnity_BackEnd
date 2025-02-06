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
            var student = await _repositoryManager.StudentRepository.GetByConditionsAsync(
                s => s.Email.ToLower().Equals(email.ToLower())
                && s.VerificationCode!= null 
                &&s.VerificationCode.Equals(code));

            var result = student != null;

            return result;
        }
    }
}
