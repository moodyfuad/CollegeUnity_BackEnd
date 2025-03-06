using CollegeUnity.Core.Dtos.AdminServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
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
                AccountStatus = newInfo.AccountStatus,
                Gender = newInfo.Gender,
            };

            return staff;
        }
    }
}
