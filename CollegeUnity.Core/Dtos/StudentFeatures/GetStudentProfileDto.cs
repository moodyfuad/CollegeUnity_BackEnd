using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.StudentFeatures
{
    public class GetStudentProfileDto
    {
        public static GetStudentProfileDto From(Student student)
        {
            return new GetStudentProfileDto(
                id : student.Id,
                firstName: student.FirstName,
                lastName: student.LastName,
                email : student.Email,
                phone: student.Phone,
                gender: student.Gender,
                birthDate: student.BirthDate,
                accountStatus: student.AccountStatus,
                accountStatusReason: student.AccountStatusReason,
                profilePicturePath: student.ProfilePicturePath,
                cardId: student.CardId,
                cardIdPicturePath: student.CardIdPicturePath,
                major: student.Major,
                level: student.Level,
                acceptanceType: student.AcceptanceType,
                isLevelEditable: student.IsLevelEditable
                );
        }

        public GetStudentProfileDto(int id, string firstName, string lastName, string email, string phone, Gender gender, DateOnly birthDate, AccountStatus accountStatus, string? accountStatusReason, string? profilePicturePath, string cardId, string? cardIdPicturePath, Major major, Level level, AcceptanceType acceptanceType, bool isLevelEditable)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Gender = gender;
            BirthDate = birthDate;
            AccountStatus = accountStatus;
            AccountStatusReason = accountStatusReason;
            ProfilePicturePath = profilePicturePath;
            CardId = cardId;
            CardIdPicturePath = cardIdPicturePath;
            Major = major;
            Level = level;
            AcceptanceType = acceptanceType;
            IsLevelEditable = isLevelEditable;
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string? AccountStatusReason { get; set; }
        public string? ProfilePicturePath { get; set; }
        public string CardId { get; set; }
        public string? CardIdPicturePath { get; set; }
        public Major Major { get; set; }
        public Level Level { get; set; }
        public AcceptanceType AcceptanceType { get; set; }
        public bool IsLevelEditable { get; set; }
    }
}
