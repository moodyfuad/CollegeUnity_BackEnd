using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StaffExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CollegeUnity.Services.AdminFeatures.Staffs
{
    public class ManageStaffFeatures : IManageStaffFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageStaffFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private async Task<string> MappingFormToProfilePicture(IFormFile profilePicture, int staffId)
        {
            var path = FileExtentionhelper.GetProfilePicturePath(staffId, profilePicture);
            await FileExtentionhelper.SaveFileAsync(path, profilePicture);
            return FileExtentionhelper.ConvertBaseDirctoryToBaseUrl(path);
        }

        public async Task<ResultDto> CreateStaffAccount(CreateStaffDto dto)
        {
            var isExist = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => s.Email == dto.Email || s.Phone == dto.Phone);
            if (isExist != null)
            {
                return new(false, "There is a staff with the same email or phone number.");
            }

            Staff staff = dto.MapTo<Staff>(null);

            await using var transaction = await _repositoryManager.BeginTransactionAsync();

            try
            {
                await _repositoryManager.StaffRepository.CreateAsync(staff);
                await _repositoryManager.SaveChangesAsync();

                if (dto.ProfilePictureFile != null)
                {
                    if (FileExtentionhelper.IsValidImage(dto.ProfilePictureFile))
                    {
                        string picturePath = await MappingFormToProfilePicture(dto.ProfilePictureFile, staff.Id);
                        staff.ProfilePicturePath = picturePath;

                        await _repositoryManager.StaffRepository.Update(staff);
                        await _repositoryManager.SaveChangesAsync();
                    }
                    else
                    {
                        return new(false, "The uploaded file is not a valid image. Please upload a picture file.");
                    }
                }

                await transaction.CommitAsync();

                return new(true, null);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ResultDto> UpdateStaffAccount(int staffId, UStaffDto dto)
        {
            var isExist = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => (s.Email == dto.Email || s.Phone == dto.Phone) && s.Id != staffId);
            if (isExist != null)
            {
                return new(false, "There is a staff with the same email or phone number.");
            }

            var staff = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => s.Id == staffId);
            if (staff == null)
            {
                return new(false, "No staff found.");
            }

            await using var transaction = await _repositoryManager.BeginTransactionAsync();

            try
            {
                _repositoryManager.Detach(staff);

                var newStaffInfo = staff.MapTo<Staff>(dto);

                if (dto.ProfilePicturePath != null)
                {
                    if (FileExtentionhelper.IsValidImage(dto.ProfilePicturePath))
                    {
                        string picturePath = await MappingFormToProfilePicture(dto.ProfilePicturePath, newStaffInfo.Id);
                        newStaffInfo.ProfilePicturePath = picturePath;

                        await _repositoryManager.StaffRepository.Update(newStaffInfo);
                        await _repositoryManager.SaveChangesAsync();
                    }
                    else
                    {
                        return new(false, "The uploaded file is not a valid image. Please upload a picture file.");
                    }
                }

                await transaction.CommitAsync();
                return new(true, null);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PagedList<GStaffByRoleDto>> GetStaffByFullName(GetStaffParameters parameters)
        {
            Expression<Func<Staff, bool>> conditions = s =>
                (s.FirstName + " " + s.LastName).StartsWith(parameters.FullName) ||
                (s.FirstName).StartsWith(parameters.FullName) ||
                (s.LastName).StartsWith(parameters.FullName)
            ;

            var staffs = await _repositoryManager.StaffRepository.GetRangeByConditionsAsync(conditions, parameters);
            return staffs.ToGStaffRoleMappers();
        }

        public async Task<bool> ChangeStaffPassword(int staffId, ChangeStaffPasswordDto dto)
        {
            var staff = await _repositoryManager.StaffRepository.GetByIdAsync(staffId);

            if (staff == null)
            {
                return false;
            }

            staff.Password = dto.password;
            staff.ConfirmPassword = dto.password;
            await _repositoryManager.StaffRepository.Update(staff);
            await _repositoryManager.SaveChangesAsync();
            return true;
        }

        public async Task<PagedList<GStaffDto>> GetStaffByRole(GetStaffParameters parameters)
        {
            Expression<Func<Staff, bool>> conditions = s => s.Roles.Contains((Roles)parameters.Role);
            var staffs = await _repositoryManager.StaffRepository.GetRangeByConditionsAsync(conditions, parameters);
            return staffs.ToGStaffMappers();
        }

        public async Task<PagedList<GStaffByRoleDto>> GetAllStaff(GetStaffParameters parameters)
        {
            var results = await _repositoryManager.StaffRepository.GetRangeAsync(parameters);
            return results.ToGStaffRoleMappers();
        }

        public async Task<ResultDto> ChangeStaffAccountStatus(int id, ChangeStaffStatusDto dto)
        {
            var staff = await _repositoryManager.StaffRepository.GetByIdAsync(id);

            if (staff == null)
            {
                return new(false, "No Staff found.");
            }

            staff.AccountStatus = dto.AccountStatus;
            staff.AccountStatusReason = dto.AccountStatusReason;
            await _repositoryManager.StaffRepository.Update(staff);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }
    }
}
