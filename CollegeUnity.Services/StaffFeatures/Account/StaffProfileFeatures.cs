using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Account;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.StaffFeatures;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Account
{
    public class StaffProfileFeatures : IStaffProfileFeatures
    {
        private readonly IRepositoryManager _repositories;
        private readonly IFilesFeatures _filesFeatures;

        public StaffProfileFeatures(IRepositoryManager repositories, IFilesFeatures filesFeatures)
        {
            _repositories = repositories;
            _filesFeatures = filesFeatures;
        }

        public async Task<ApiResponse<GetStaffProfileDto?>> GetInfo(int staffId)
        {
            var staff = await _repositories.StaffRepository.GetByIdAsync(staffId);

            if (staff == null)
            {
                return ApiResponse<GetStaffProfileDto>.NotFound();
            }

            return ApiResponse<GetStaffProfileDto>.Success(GetStaffProfileDto.From(staff));
        }

        public async Task<ApiResponse<bool>> Update(int staffId, UpdateUserProfileDto dto)
        {
            var staff = await _repositories.StaffRepository.GetByConditionsAsync(
                condition: s => s.Id == staffId,
                trackChanges: true);

            if (staff is null)
            {
                throw new KeyNotFoundException("Student Not Found");
            }

            if (dto.Email is not null)
            {
                staff.Email = dto.Email;
            }

            if (dto.Phone is not null)
            {
                staff.Phone = dto.Phone;
            }

            if (dto.UpdateImage)
            {
                staff.ProfilePicturePath = await GetProfilePicturePath(dto.ProfilePicture);
            }

            await _repositories.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
        }

        public async Task<ApiResponse<bool>> UpdatePassword(int staffId, UpdateUserPasswordDto dto)
        {
            var staff = await _repositories.StaffRepository.GetByConditionsAsync(
                condition: s => s.Id == staffId,
                trackChanges: true);

            if (staff is null)
            {
                throw new KeyNotFoundException("Student Not Found");
            }

            if (dto.Old != staff.Password)
            {
                return ApiResponse<bool>.BadRequest("Old Password Does Not Match");
            }

            staff.Password = dto.New;
            staff.ConfirmPassword = dto.New;

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
