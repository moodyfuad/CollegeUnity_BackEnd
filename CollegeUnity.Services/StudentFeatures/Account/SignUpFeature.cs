using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StudentFeatures.Account;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StudentExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IFilesFeatures _filesFeatures;

        public SignUpFeature(
            IRepositoryManager repositoryManager,
            IFilesFeatures filesFeatures
        )
        {
            _repositoryManager = repositoryManager;
            _filesFeatures = filesFeatures;
        }

        public async Task<ApiResponse<string?>> SignUpStudent(StudentSignUpDto studentDto)
        {
            if (studentDto.CardIdPictureFile == null ||
                studentDto.CardIdPictureFile.Length == 0)
            {
                return ApiResponse<string>.BadRequest("Sign up failed", ["Invalid Registration Id Card Picture"]);
            }

            Student? student = await _repositoryManager.StudentRepository.GetByConditionsAsync(
                s =>
                    s.Email.ToLower().Equals(studentDto.Email.ToLower()) ||
                    s.CardId.Equals(studentDto.CardId) ||
                    s.Phone.Equals(studentDto.Phone),
                trackChanges: true);

            if (student is not null)
            {

                List<string> errors = [];
                if (student.CardId == studentDto.CardId)
                {
                    errors.Add($"Student Already exist with [ {student.CardId} ] Registration ID");
                }

                if (student.Email.ToLower().Equals(studentDto.Email.ToLower()))
                {
                    errors.Add("Email Already in use");
                }

                if (student.Phone.Equals(studentDto.Phone))
                {
                    errors.Add("The Phone number Already in use");
                }

                if (student.AccountStatus is AccountStatus.Denied)
                {
                    string? cardIdPicturePath = await GetCardIdPicturePath(studentDto.CardIdPictureFile);
                    string? profilePicturePath = await GetProfilePicturePath(studentDto.ProfilePictureFile);

                    student = student!.MapFrom<StudentSignUpDto>(studentDto, cardIdPicturePath, profilePicturePath);

                    return await CreateStudent(student, forCreate: false);
                }
                else
                {
                    return ApiResponse<string>.BadRequest("Sign up failed", errors);
                }

            }
            else
            {
                string? cardIdPicturePath = await GetCardIdPicturePath(studentDto.CardIdPictureFile);
                string? profilePicturePath = await GetProfilePicturePath(studentDto.ProfilePictureFile);

                student = student.MapFrom<StudentSignUpDto>(studentDto, cardIdPicturePath, profilePicturePath);
                return await CreateStudent(student, forCreate: true);
            }
        }

        private async Task<ApiResponse<string?>> CreateStudent(Student student, bool forCreate = true)
        {
            string msg = "Sign up Information Updated";
            if (forCreate)
            {
                // TODO: password hashing 
                var passwordHasher = new PasswordHasher<User>();
                string hashedPassword = passwordHasher.HashPassword(student, student.Password);
                student.Password = hashedPassword;
                student.ConfirmPassword = hashedPassword;
                student = await _repositoryManager.StudentRepository.CreateAsync(student);
                msg = "Sign up Success";
            }

            try
            {
                await _repositoryManager.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ApiResponse<string?>.InternalServerError("Sign up failed", [ex.Message]);
            }

            return ApiResponse<string>.Success(null, msg);
        }

        private async Task<string?> GetCardIdPicturePath(IFormFile imageFile)
        {
            bool isImageValid = FileExtentionhelper.IsValidImage(imageFile);
            return isImageValid ?
                await _filesFeatures.MappingFormToCardPicture(imageFile) :
                null;
        }

        private async Task<string?> GetProfilePicturePath(IFormFile imageFile)
        {
            return FileExtentionhelper.IsValidImage(imageFile) ?
                await _filesFeatures.MappingFormToProfilePicture(imageFile) :
                null;
        }
    }
}
