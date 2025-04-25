using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StudentExtensions
{
    public static partial class StudentExtensions
    {
        public static Student MapFrom<T>(
            this Student? student,
            StudentSignUpDto dto,
            string CardIdPicturePath,
            string? ProfilePicturePath)
            where T : StudentSignUpDto
        {


            return new()
            {
                CardId = dto.CardId,
                CardIdPicturePath = CardIdPicturePath,

                ProfilePicturePath = ProfilePicturePath,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,

                AccountStatus = Enums.AccountStatus.Waiting,
                AccountStatusReason = student is null ? null : "Updated Student Sign Up Information",

                BirthDate = dto.BirthDate,

                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword,
                VerificationCode = null,

                Phone = dto.Phone,
                Email = dto.Email,

                Major = dto.Major,
                Level = dto.Level,
                AcceptanceType = dto.AcceptanceType,
                Gender = dto.Gender,

                EditedAt = student is null ? null : DateTime.UtcNow.ToLocalTime(),
                CreatedAt = student is null ? DateTime.UtcNow.ToLocalTime(): student.CreatedAt,

                IsLevelEditable = false,

            };

        }
    }
}
