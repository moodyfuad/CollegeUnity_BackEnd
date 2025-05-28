using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StaffExtensions;
using LinqKit;
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
        private readonly IFilesFeatures _filesFeatures;
        public ManageStaffFeatures(IRepositoryManager repositoryManager, IFilesFeatures postFilesFeatures)
        {
            _repositoryManager = repositoryManager;
            _filesFeatures = postFilesFeatures;
        }

        public async Task<ResultDto> CreateStaffAccount(CreateStaffDto dto)
        {
            var isExist = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => s.Email.ToLower() == dto.Email.ToLower() || s.Phone == dto.Phone);
            if (isExist != null)
            {
                return new(false, "There is a staff with the same email or phone number.");
            }

            Staff staff = dto.MapTo<Staff>(null);

            await using var transaction = await _repositoryManager.BeginTransactionAsync();

            try
            {
                await _repositoryManager.StaffRepository.CreateAsync(staff);
                var password = PasswordHasherHelper.Hash(staff, dto.Password);
                staff.Password = password;
                staff.ConfirmPassword = password;
                await _repositoryManager.SaveChangesAsync();

                if (dto.ProfilePictureFile != null)
                {
                    if (FileExtentionhelper.IsValidImage(dto.ProfilePictureFile))
                    {
                        string picturePath = await _filesFeatures.MappingFormToProfilePicture(dto.ProfilePictureFile);
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
            var isExist = await _repositoryManager.StaffRepository
                .GetByConditionsAsync(s => (s.Email == dto.Email || s.Phone == dto.Phone) && s.Id != staffId);

            if (isExist != null)
                return new(false, "There is a staff with the same email or phone number.");

            var staff = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => s.Id == staffId);
            if (staff == null)
                return new(false, "No staff found.");

            await using var transaction = await _repositoryManager.BeginTransactionAsync();

            try
            {
                _repositoryManager.Detach(staff);
                var newStaffInfo = staff.MapTo<Staff>(dto);

                if (dto.ProfilePicturePath != null)
                {
                    if (FileExtentionhelper.IsValidImage(dto.ProfilePicturePath))
                    {
                        string picturePath = await _filesFeatures.MappingFormToProfilePicture(dto.ProfilePicturePath);
                        newStaffInfo.ProfilePicturePath = picturePath;
                    }
                    else
                    {
                        return new(false, "The uploaded file is not a valid image. Please upload a picture file.");
                    }
                }

                await _repositoryManager.StaffRepository.Update(newStaffInfo);
                await _repositoryManager.SaveChangesAsync();

                await transaction.CommitAsync();
                return new(true, null);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<PagedList<GStaffByRoleDto>> GetStaffs(GetStaffParameters parameters)
        {
            Expression<Func<Staff, bool>> conditions = s => true;

            if (parameters.Role.HasValue)
            {
                conditions = conditions.And(r => r.Roles.Contains(parameters.Role.Value));
            }

            if (!string.IsNullOrEmpty(parameters.FullName))
            {
                conditions = conditions.And(s =>
                    (s.FirstName + " " + s.MiddleName + " " + s.LastName).StartsWith(parameters.FullName));
            }

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

            var password = PasswordHasherHelper.Hash(staff, dto.password);
            staff.Password = password;
            staff.ConfirmPassword = password;
            await _repositoryManager.StaffRepository.Update(staff);
            await _repositoryManager.SaveChangesAsync();
            return true;
        }

        public async Task<ResultDto> ChangeUserAccountStatus(int id, ChangeUserStatusDto dto)
        {
            var user = await _repositoryManager.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                return new(false, "No Student found.");
            }

            user.AccountStatus = dto.AccountStatus;
            user.AccountStatusReason = dto.AccountStatusReason;
            await _repositoryManager.UserRepository.Update(user);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }
    }
}
