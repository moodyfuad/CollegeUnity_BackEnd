using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StudentFeatures.Account;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StudentExtensions;
using CollegeUnity.Core.MappingExtensions.StudentExtensions.Get;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Account
{
    public class StudentProfileFeatures : IStudentProfileFeatures
    {
        private readonly IRepositoryManager _repositories;
        private readonly IFilesFeatures _filesFeatures;

        public StudentProfileFeatures(IRepositoryManager repositoryManager, IFilesFeatures filesFeatures)
        {
            _repositories = repositoryManager;
            _filesFeatures = filesFeatures;
        }

        public async Task<ApiResponse<GetStudentProfileDto?>> GetInfo(int studentId)
        {
           var student = await _repositories.StudentRepository.GetByIdAsync(studentId);

            if (student == null)
            {
                return ApiResponse<GetStudentProfileDto>.NotFound();
            }

            return ApiResponse<GetStudentProfileDto>.Success(GetStudentProfileDto.From(student));
        }

        public async Task<ApiResponse<bool>> Update(int studentId, UpdateUserProfileDto dto)
        {
            var student = await _repositories.StudentRepository.GetByConditionsAsync(
                condition: s => s.Id == studentId,
                trackChanges: true);

            if (student is null)
            {
                throw new KeyNotFoundException("Student Not Found");
            }

            if (dto.Email is not null)
            {
                student.Email = dto.Email;
            }

            if (dto.Phone is not null)
            {
                student.Phone = dto.Phone;
            }

            if (dto.UpdateImage)
            {
                student.ProfilePicturePath = await GetProfilePicturePath(dto.ProfilePicture);
            }

            await _repositories.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<bool>> UpdatePassword(int studentId, UpdateUserPasswordDto dto)
        {
            var student = await _repositories.StudentRepository.GetByConditionsAsync(
                condition: s => s.Id == studentId,
                trackChanges: true);

            if (student is null)
            {
                throw new KeyNotFoundException("Student Not Found");
            }

            if (dto.Old != student.Password)
            {
                return ApiResponse<bool>.BadRequest("Old Password Does Not Match");
            }

            student.Password = dto.New;
            student.ConfirmPassword = dto.New;

            await _repositories.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }

        private async Task<string?> GetProfilePicturePath(IFormFile imageFile)
        {
            return FileExtentionhelper.IsValidImage(imageFile) ?
                await _filesFeatures.MappingFormToProfilePicture(imageFile) :
                null;
        }
    }
}
