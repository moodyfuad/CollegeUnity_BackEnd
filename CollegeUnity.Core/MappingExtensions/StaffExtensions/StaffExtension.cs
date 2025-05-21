using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StaffExtensions
{
    public partial class StaffExtension
    {
        public static Staff MapTo<T>(this Staff oldInfo, UStaffDto newInfo) where T : Staff
        {
            Staff staff = new()
            {
                Id = oldInfo.Id,
                FirstName = newInfo.FirstName,
                LastName = newInfo.LastName,
                MiddleName = newInfo.MiddleName,
                Password = oldInfo.Password,
                ConfirmPassword = oldInfo.ConfirmPassword,
                ProfilePicturePath = oldInfo.ProfilePicturePath,
                Email = newInfo.Email,
                Phone = newInfo.Phone,
                Roles = newInfo.Roles,
                BirthDate = newInfo.BirthDate,
                EducationDegree = newInfo.EducationDegree,
                AccountStatus = oldInfo.AccountStatus,
                Gender = newInfo.Gender,
            };

            return staff;
        }

        #region For GStaffDto
        private static GStaffDto MapTo(this Staff staff)
        {
            return new()
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                MiddleName = staff.MiddleName,
                LastName = staff.LastName,
                Phone = staff.Phone,
                BirthDate = staff.BirthDate,
                Email = staff.Email,
                EducationDegree = ConvertToArabicHelper.EducationToArabic(staff.EducationDegree),
                Gender = ConvertToArabicHelper.GenderToArabic(staff.Gender),
                AccountStatus = staff.AccountStatus.ToString(),
                profilePicturePath = staff.ProfilePicturePath
            };
        }

        public static PagedList<GStaffDto> ToGStaffMappers(this PagedList<Staff> staffs)
        {
            var results = staffs.Select(s => s.MapTo()).ToList();
            var pagedList = new PagedList<GStaffDto>
            (
                items: results,
                count: staffs.Count(),
                pageNumber: staffs.CurrentPage,
                pageSize: staffs.PageSize
            );
            return pagedList;
        }
        #endregion

        #region For GStaffByRoleDto
        private static GStaffByRoleDto MapToRole(this Staff staff)
        {
            return new()
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                MiddleName = staff.MiddleName,
                LastName = staff.LastName,
                Phone = staff.Phone,
                BirthDate = staff.BirthDate,
                Email = staff.Email,
                EducationDegree = ConvertToArabicHelper.EducationToArabic(staff.EducationDegree),
                Gender = ConvertToArabicHelper.GenderToArabic(staff.Gender),
                profilePicturePath = staff.ProfilePicturePath,
                AccountStatus = staff.AccountStatus.ToString(),
                roles = staff.Roles.ToDictionary(
                    role => (int)role,
                    role => role.ToString()
                )
            };
        }

        public static PagedList<GStaffByRoleDto> ToGStaffRoleMappers(this PagedList<Staff> staffs)
        {
            var results = staffs.Select(s => s.MapToRole()).ToList();
            var pagedList = new PagedList<GStaffByRoleDto>
            (
                items: results,
                count: staffs.Count(),
                pageNumber: staffs.CurrentPage,
                pageSize: staffs.PageSize
            );
            return pagedList;
        }
        #endregion
    }
}
