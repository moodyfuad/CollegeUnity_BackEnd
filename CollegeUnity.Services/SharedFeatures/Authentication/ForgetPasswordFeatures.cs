using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.ForgetPasswordFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using EmailService;
using EmailService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Authentication
{
    public class ForgetPasswordFeatures : IForgetPasswordFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _config;

        public ForgetPasswordFeatures(
            IRepositoryManager repositoryManager,
            IEmailServices emailServices,
            IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _emailServices = emailServices;
            _config = configuration;
        }

        public async Task<ForgetPasswordFeatureResultDto> SendResetPasswordCode(string email)
        {
            try
            {
                var userResult = await this._GetUserByEmail(email);
                if (userResult == null)
                {
                    return ForgetPasswordFeatureResultDto.
                        Failed($"No User Found With [{email}] Email Address)");
                }

                string fullname = $"{userResult.FirstName} {userResult.LastName}";

                var result = await this._emailServices.ForgetPassword(fullname, userResult.Email);

                if (result.IsSuccess == false)
                {
                    return ForgetPasswordFeatureResultDto.
                       Failed(result.Message);
                }
                else
                {
                    userResult.VerificationCode = result.GetResetCode();

                    await this._repositoryManager.UserRepository.Update(userResult);

                    await this._repositoryManager.SaveChangesAsync();

                    string token = JwtHelpers.CreateForgetPasswordToken(
                        userResult.Email,
                        ForgetPasswordRoles.CodeSent,
                        _config,
                        DateTime.Now.AddMinutes(5));

                    result.SetToken(token);

                    return ForgetPasswordFeatureResultDto.Sent(token);
                }
            }
            catch (Exception ex)
            {
                return ForgetPasswordFeatureResultDto.Failed($"Error : {ex.Message}");
            }
        }

        public async Task<ForgetPasswordFeatureResultDto> ResetPassword(string email, string newPassword)
        {
            var user = await _GetUserByEmail(email);
            try
            {
                user.Password = newPassword;
                user.ConfirmPassword = newPassword;
                user = await _repositoryManager.UserRepository.Update(user);
                await _repositoryManager.SaveChangesAsync();

                return ForgetPasswordFeatureResultDto.Reset();
            }
            catch (Exception ex)
            {
                return ForgetPasswordFeatureResultDto.Failed($"Error : {ex.Message}");
            }
        }


        public async Task<ForgetPasswordFeatureResultDto> ValidateVerificationCode(string email, string code)
        {
            var user = await _GetUserByEmail(email);
            if (user == null || code == null || user.VerificationCode == null)
            {
                return ForgetPasswordFeatureResultDto.Failed("Failed Verifying The Code");
            }

            if (user.VerificationCode.ToLower() != code.ToLower())
            {
                return ForgetPasswordFeatureResultDto.Failed("Code Does Not Match");
            }

            user.VerificationCode = string.Empty;
            await _repositoryManager.UserRepository.Update(user);
            await _repositoryManager.SaveChangesAsync();

            var expiresAt = DateTime.Now.AddMinutes(5);

            string token = JwtHelpers.CreateForgetPasswordToken(
                email,
                ForgetPasswordRoles.ResetAllowed,
                _config,
                expiresAt
                );

            return ForgetPasswordFeatureResultDto.Sent(token, "Go To Reset Password Link");
        }

        private async Task<User> _GetUserByEmail(string email)
        {
            var user = await _repositoryManager.UserRepository.GetByEmail(email);
            return user;
        }
    }
}
