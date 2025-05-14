using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.StaffFeatures
{
    public class GetStaffProfileDto
    {
        public GetStaffProfileDto(int id, string firstName, string middleName, string lastName, string email, string phone, Gender gender, DateOnly birthDate, AccountStatus accountStatus, string? accountStatusReason, string? profilePicturePath, ICollection<Roles> roles, EducationDegree educationDegree)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Gender = gender;
            BirthDate = birthDate;
            AccountStatus = accountStatus;
            AccountStatusReason = accountStatusReason;
            ProfilePicturePath = profilePicturePath;
            Roles = roles;
            EducationDegree = educationDegree;
        }

        public static GetStaffProfileDto From (Staff staff)
        {
            return new GetStaffProfileDto(
                id : staff.Id,
                firstName : staff.FirstName,
                middleName : staff.MiddleName,
                lastName : staff.LastName,
                email : staff.Email,
                phone : staff.Phone,
                gender : staff.Gender,
                birthDate : staff.BirthDate,
                accountStatus : staff.AccountStatus,
                accountStatusReason : staff.AccountStatusReason,
                profilePicturePath : staff.ProfilePicturePath,
                roles : staff.Roles,
                educationDegree : staff.EducationDegree
                );
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string? AccountStatusReason { get; set; }
        public string? ProfilePicturePath { get; set; }
        public ICollection<Roles> Roles { get; set; } = [];
        public EducationDegree EducationDegree { get; set; }

    }
}
