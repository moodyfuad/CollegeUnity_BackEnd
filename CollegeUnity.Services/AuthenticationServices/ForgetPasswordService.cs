using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using EmailService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AuthenticationServices
{
    public partial class AuthenticationService
    {
        private async Task<User> _GetUserByEmail(string email)
        {
            var student = await _repositoryManager.StudentRepository.GetByEmail(email);
            return student != null ? student : await _repositoryManager.StaffRepository.GetByEmail(email);
        }
        
        
        
        private async Task<Result> _SendResetPasswordRequest(string email,Roles role,DateTime expiresAt)
        {
            
            try
            {
                var userResult = await _GetUserByEmail(email);
                if (userResult != null)
                {
                    string fullname = $"{userResult.FirstName} {userResult.LastName}";
                    var result = await _emailServices.ForgetPassword(fullname, userResult.Email);

                    if (result.IsSuccess == false)
                    {
                        return result;
                    }
                    else
                    {
                        userResult.VerificationCode = result.GetResetCode();
                        switch (userResult)
                        {
                            case Student student:
                                await _repositoryManager.StudentRepository.Update(student);
                                break;
                            case Staff staff:
                                await _repositoryManager.StaffRepository.Update(staff);
                                break;
                            default:
                                break;
                        }

                        await _repositoryManager.SaveChangesAsync();

                        result.SetToken(
                            CreateForgetPasswordToken(userResult.Email, role, expiresAt));

                        return userResult != null ? result : Result.Fail("Something Went Wrong!");
                    }
                }
                else return Result.Fail($"No User Found With [{email}] Email Address");
            }
            catch (Exception ex)
            {
                return Result.Fail($"error {ex.Message}");
            }
        }

        private string CreateForgetPasswordToken(
            in string email,
            in Roles role,
            in DateTime? expireAt = null)
        {
            JwtSecurityToken token = new(
            issuer: _config[$"Jwt:{JwtKeys.Issuer}"],
            audience: _config[$"Jwt:{JwtKeys.Audience}"],
            signingCredentials: CreateSigningCredentials(in _config),

            claims: new List<Claim>()
            {
                new Claim(CustomClaimTypes.Role, role.ToString()),
                new Claim(CustomClaimTypes.Role, AuthenticationRoles.ForgotPassword.ToString()),
                new Claim(CustomClaimTypes.Email, email),
            },

            expires: expireAt ?? DateTime.Now.AddMinutes(5)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
