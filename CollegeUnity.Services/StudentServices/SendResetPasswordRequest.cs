using EmailService;
using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentServices
{
    public partial class StudentService
    {
        private async Task<Result> _SendResetPasswordRequest(string email)
        {

            try
            {
                var studentResult = await _repositoryManager.StudentRepository.GetByConditionsAsync(
                s => s.Email.ToLower().Equals(email.ToLower()));

                if (studentResult != null)
                {
                    string fullname = $"{studentResult.FirstName} {studentResult.LastName}";
                    var result = await _emailServices.ForgetPassword(fullname, studentResult.Email);
                    result.ResetCode = "";
                    

                    if (!result.IsSuccess)
                    {
                        return result;
                    }
                    else
                    {
                        studentResult.VerificationCode = result.ResetCode;
                        studentResult = await _repositoryManager.StudentRepository.Update(studentResult);
                        await _repositoryManager.SaveChangesAsync();
                        return studentResult != null ? result : Result.Fail("Something Went Wrong!");
                    }
                }
                else return Result.Fail($"No Student Found With [{email}] Email Address");
            }
            catch (Exception ex)
            {
                return Result.Fail($"error {ex.Message}");
            }
        }
    }
}
