using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class RegisteredStudentForCoursesResultDto
    {
        public RegisteredStudentForCoursesResultDto(int id, string cardId, string firstName, string middleName, string lastName, string phone, Gender gender, Major major, Level level, AcceptanceType acceptanceType, string? profilePicturePath)
        {
            Id = id;
            CardId = cardId ?? throw new ArgumentNullException(nameof(cardId));
            FullName = $"{firstName} {middleName} {lastName}";
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Gender = gender;
            Major = major;
            Level = level;
            AcceptanceType = acceptanceType;
            ProfilePicturePath = profilePicturePath;
        }

        public int Id { get; set; }
        public string CardId { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public Major Major { get; set; }
        public Level Level { get; set; }
        public AcceptanceType AcceptanceType { get; set; }
        public string? ProfilePicturePath { get; set; }

        public static RegisteredStudentForCoursesResultDto MapFrom(Student student)
        {
            RegisteredStudentForCoursesResultDto result = new(
                id: student.Id,
                cardId: student.CardId,
                firstName: student.FirstName,
                middleName: student.MiddleName,
                lastName: student.LastName,
                phone: student.Phone,
                gender: student.Gender,
                major: student.Major,
                level: student.Level,
                acceptanceType: student.AcceptanceType,
                profilePicturePath: student.ProfilePicturePath);

            return result;
        }

    }
}
