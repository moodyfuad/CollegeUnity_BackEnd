using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StaffExtensions
{
    public static partial class StaffExtension
    {
        public static Staff MapTo<T>(this CreateStaffDto dto) where T : Staff
        {
            return new()
            {
                ProfilePicturePath = dto.ProfilePicturePath,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword,
                Roles = dto.Roles,
                BirthDate = dto.BirthDate,
                EducationDegree = dto.EducationDegree,
                AccountStatus = AccountStatus.Active,
                Gender = dto.Gender,
            };
        }
        
        public static CreateStaffDto MapTo<T>(this Staff staff) where T : CreateStaffDto
        {
            return new()
            {
                ProfilePicturePath = staff.ProfilePicturePath,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                MiddleName = staff.MiddleName,
                Email = staff.Email,
                Phone = staff.Phone,
                Password = staff.Password,
                ConfirmPassword = staff.ConfirmPassword,
                Roles = staff.Roles,
                BirthDate = staff.BirthDate,
                EducationDegree = staff.EducationDegree,
                Gender = staff.Gender,
            };
        }
    }
}
