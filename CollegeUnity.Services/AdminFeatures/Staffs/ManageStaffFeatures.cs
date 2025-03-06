using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StaffExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            return path;
        }

        public async Task<bool> CreateStaffAccount(CreateStaffDto dto)
        {
            var isExist = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => s.Email == dto.Email && s.Phone == dto.Phone);
            if (isExist != null)
            {
                return false;
            }

            Staff staff = dto.MapTo<Staff>(null);

            await using var transaction = await _repositoryManager.BeginTransactionAsync();

            try
            {
                await _repositoryManager.StaffRepository.CreateAsync(staff);
                await _repositoryManager.SaveChangesAsync();

                if (dto.ProfilePicturePath != null)
                {
                    string picturePath = await MappingFormToProfilePicture(dto.ProfilePicturePath, staff.Id);
                    staff.ProfilePicturePath = picturePath;

                    _repositoryManager.StaffRepository.Update(staff);
                    await _repositoryManager.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateStaffAccount(int staffId, UStaffDto dto)
        {
            var isExist = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => (s.Email == dto.Email || s.Phone == dto.Phone) && s.Id != staffId);
            if (isExist != null)
            {
                return false;
            }

            var staff = await _repositoryManager.StaffRepository.GetByConditionsAsync(s => s.Id == staffId);
            if (staff == null)
            {
                return false;
            }

            _repositoryManager.Detach(staff);

            var newStaffInfo = staff.MapTo<Staff>(dto);

            if (dto.ProfilePicturePath != null)
            {
                string picturePath = await MappingFormToProfilePicture(dto.ProfilePicturePath, newStaffInfo.Id);
                newStaffInfo.ProfilePicturePath = picturePath;
            }

            await _repositoryManager.StaffRepository.Update(newStaffInfo);
            await _repositoryManager.SaveChangesAsync();

            return true;
        }
    }
}
