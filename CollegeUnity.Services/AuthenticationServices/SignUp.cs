using CollegeUnity.Core.Constants;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.StudentExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AuthenticationServices
{
    public partial class AuthenticationService
    {

       public async Task<string> SignUpStudent(StudentSignUpDto studentDto)
        {
            Student student = await _repositoryManager
                        .StudentRepository.
                        GetByConditionsAsync(
                        s => s.Email.ToLower() == studentDto.Email.ToLower() ||
                        s.CardId == studentDto.CardId ||
                        s.Phone == studentDto.Phone);
            if (student != null)
            {
                return student.CardId == studentDto.CardId ? $"User Already exist with [ {student.CardId} ] ID" :
                 student.Email.ToLower() == studentDto.Email.ToLower() ? "Email Already in use" :
                 student.Phone == studentDto.Phone ? "The Phone number Already in use" :
                 "One or more Invalid field";
            }

            else
                student = student.MapFrom<StudentSignUpDto>(studentDto);

            student = await _repositoryManager.StudentRepository.CreateAsync(student);
            try
            {
                await _repositoryManager.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return "Something went wrong :" + ex.Message;
            }
            return "User Created Successfully, please login!";
        }
    }
}
