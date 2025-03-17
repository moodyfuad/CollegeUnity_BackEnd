using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Request;
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
    public class RequestsFeature : IRequestsFeature
    {
        private readonly IRepositoryManager _repositories;

        public RequestsFeature(IRepositoryManager repositoryManager)
        {
            _repositories = repositoryManager;
        }

        public async Task<ApiResponse<string?>> Send(int studentId, int staffId, SendRequestDto sendRequestDto)
        {
            string failedMsg = "Failed Sending The Request";
            var staff = await _repositories.StaffRepository.GetByIdAsync(staffId);
            if (staff == null)
            {
                return ApiResponse<string?>.BadRequest(failedMsg, ["Staff Member Not Found"]);
            }

            var student = await _repositories.StudentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return ApiResponse<string?>.BadRequest(failedMsg, ["Student Not Found"]);
            }

            var request = CreateRequestObj(student, staff, sendRequestDto);

            // there is a null reference here in the Requests list [fix it]
            student.Requests ??= [];

            student.Requests.Add(request);
            try
            {
                await _repositories.StudentRepository.Update(student);

                await _repositories.SaveChangesAsync();

                return ApiResponse<string>.Success(null, message: "Request Send Successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string?>.InternalServerError(ex.Message, [ex.InnerException?.Message]);
            }
        }

        private static Request CreateRequestObj(Student student, Staff staff, SendRequestDto dto)
        {
            var request = new Request()
            {
                Title = dto.Title,
                Content = dto.Content,
                Staff = staff,
                Student = student
            };

            return request;
        }

        public async Task<ApiResponse<PagedList<GetStudentRequestsDto>?>> Get(
            int studentId,
            GetStudentRequestsQueryString queryString)
        {
            var student = await _repositories.StudentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return ApiResponse<PagedList<GetStudentRequestsDto>>.BadRequest("Failed Retrieving The Requests", ["Student Not Found"]);
            }

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
                includes:
                    request => request.Staff);

            requests.MapTo(out var result);

            if (requests != null && requests.Count == 0)
            {
                return ApiResponse<PagedList<GetStudentRequestsDto>>.Success(result, "No Requests To Retrieve");
            }


            return ApiResponse<PagedList<GetStudentRequestsDto>>.Success(result, $"[{result.Count}] records retrieved.");
        }
    }
}
