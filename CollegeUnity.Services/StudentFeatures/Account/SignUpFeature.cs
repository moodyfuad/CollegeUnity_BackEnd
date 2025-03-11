using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Account;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StudentExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Account
{
    public class SignUpFeature : ISignUpFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public SignUpFeature(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ApiResponse<string?>> SignUpStudent(StudentSignUpDto studentDto)
        {
            if (studentDto.CardIdPictureFile == null ||
                studentDto.CardIdPictureFile.Length == 0)
            {
                return ApiResponse<string>.BadRequest("Sign up failed", ["Invalid Registration Id Card Picture"]);
            }

            Student student = await _repositoryManager
                        .StudentRepository.
                        GetByConditionsAsync(
                        s => s.Email.ToLower() == studentDto.Email.ToLower() ||
                        s.CardId == studentDto.CardId ||
                        s.Phone == studentDto.Phone);
            if (student != null)
            {
                return student.CardId == studentDto.CardId ?
                    ApiResponse<string>.BadRequest("Sign up failed", [$"User Already exist with [ {student.CardId} ] ID"]) :

                 student.Email.ToLower() == studentDto.Email.ToLower() ?
                 ApiResponse<string>.BadRequest("Sign up failed", ["Email Already in use"]) :

                 student.Phone == studentDto.Phone ?
                 ApiResponse<string>.BadRequest("Sign up failed", ["The Phone number Already in use"]) :

                 ApiResponse<string>.BadRequest("Sign up failed", ["One or more Invalid field"]);
            }
            else
            {
                string? cardIdPicturePath = await GetCardIdPicturePath(studentDto.CardIdPictureFile);
                string? profilePicturePath = await GetProfilePicturePath(studentDto.ProfilePicturePath);

                student = student!.MapFrom<StudentSignUpDto>(studentDto, cardIdPicturePath, profilePicturePath);
            }

            student = await _repositoryManager.StudentRepository.CreateAsync(student);
            try
            {
                await _repositoryManager.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.InternalServerError("Sign up failed", [ex.Message]);
            }

            return ApiResponse<string>.Success(null, "Sign up Success");
        }

        private async Task<string?> GetCardIdPicturePath(IFormFile imageFile)
        {
            return FileExtentionhelper.IsValidImage(imageFile) ?
                await FileExtentionhelper.SaveCardIdPictureFile(imageFile) :
                null;
        }

        private async Task<string?> GetProfilePicturePath(IFormFile imageFile)
        {
            return FileExtentionhelper.IsValidImage(imageFile) ?
                await FileExtentionhelper.SaveProfilePictureFile(imageFile) :
                null;
        }
    }
}
