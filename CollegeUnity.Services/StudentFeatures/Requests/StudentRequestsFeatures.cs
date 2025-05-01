using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Request;
using CollegeUnity.Core.CustomExceptions;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Requests
{
    public class StudentRequestsFeatures : IStudentRequestsFeatures
    {
        private readonly IRepositoryManager _repositories;

        public StudentRequestsFeatures(IRepositoryManager repositoryManager)
        {
            _repositories = repositoryManager;
        }

        public async Task<ApiResponse<string?>> Send(int studentId, int staffId, SendRequestDto sendRequestDto)
        {
            string failedMsg = "Failed Sending The Request";
            var staff = await _repositories.StaffRepository.GetByIdAsync(staffId) ??
                throw new BadRequestException(failedMsg, ["Staff Member Not Found"]);

            var student = await _repositories.StudentRepository.GetByIdAsync(studentId) ??
                throw new BadRequestException(failedMsg, ["Student Not Found"]);

            var request = CreateRequestObj(student, staff, sendRequestDto);

            // there is a null reference here in the Requests list [fix it]
            student.Requests ??= [];

            student.Requests.Add(request);
            try
            {
                await _repositories.StudentRepository.Update(student);

                await _repositories.SaveChangesAsync();

                return ApiResponse<string>.Success(null, message: "Request Sent Successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string?>.InternalServerError(ex.Message, [ex.InnerException?.Message]);
            }
        }

        private static Request CreateRequestObj(Student student, Staff staff, SendRequestDto dto)
        {
            return new Request()
            {
                Title = dto.Title,
                Content = dto.Content,
                Staff = staff,
                Student = student
            };
        }

        public async Task<ApiResponse<PagedList<GetUserRequestsDto>?>> Get(
            int studentId,
            GetStudentRequestsQueryString queryString)
        {
            var student = await _repositories.StudentRepository.GetByIdAsync(studentId) ??
                throw new BadRequestException("Failed Retrieving The Requests", ["Student Not Found"]);

            var nameContains = new Func<string, string, bool>((name, searchValue) =>
            {
                if (string.IsNullOrEmpty(searchValue) || string.IsNullOrWhiteSpace(searchValue))
                {
                    return true;
                }

                return name.ToLower().Contains(searchValue.ToLower());
            });
            List<string> fullName = queryString.StaffName.Split(' ').ToList();
            string firstName = fullName.Count > 0 ? fullName[0] : string.Empty;
            string middleName = fullName.Count > 1 ? fullName[1] : string.Empty;
            string lastName = fullName.Count > 2 ? fullName[2] : string.Empty;

            var requests = await _repositories.RequestRepository.GetRangeByConditionsAsync(
                condition: request =>
                    request.StudentId == studentId &&
                    request.Staff.FirstName.Contains(firstName) &&
                    request.Staff.MiddleName.Contains(middleName) &&
                    request.Staff.LastName.Contains(lastName),
                queryStringParameters: queryString,
                includes: request => request.Staff);


            if (requests is null || requests.Count == 0)
            {
                return ApiResponse<PagedList<GetUserRequestsDto>?>.Success(null, "No Requests To Retrieve");
            }

            // todo: delete all its implementation
            #warning requests.MapTo(out var result);
            var result = requests.To<GetUserRequestsDto>(RequestExtensions.MappingFun);

            return ApiResponse<PagedList<GetUserRequestsDto>?>.Success(result, $"[{result.Count}] records retrieved.");
        }
    }
}
