using CollegeUnity.Core.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities.Extensions
{
    public static class StudentExtension
    {
        public static Student MapFrom(this  Student student, StudentSignUpDto dto)
        {
            
            return new()
            {
                CardId = dto.CardId,
                CardIdPicturePath = dto.CardIdPicturePath,
                
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                
                AccountStatus = Enums.AccountStatus.Waiting,
                AccountStatusReason = null,
                
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

                EditedAt = null,
                CreatedAt = DateTime.UtcNow,

                IsLevelEditable = false,
                
            };
        
        }
    }
}
