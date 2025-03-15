using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Request;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public async Task<ApiResponse<ICollection<GetStudentRequestsDto>?>> Get(int studentId)
        {
            var student = await _repositories.StudentRepository.GetByConditionsAsync(
                 condition: s => s.Id.Equals(studentId),
                 includes: s => s.Requests);
            if (student == null)
            {
                return ApiResponse<ICollection<GetStudentRequestsDto>>.BadRequest("Failed Retrieving The Requests", ["Student Not Found"]);
            }

            if (student.Requests == null || student.Requests.Count == 0)
            {
                return ApiResponse<ICollection<GetStudentRequestsDto>>.Success(null, "No Requests To Retrieve");
            }
            var result = await MapToGetStudentRequestsDto(student.Requests);

            return ApiResponse<ICollection<GetStudentRequestsDto>>.Success(result, $"[{result.Count}] records retrieved.");
        }

        private async Task<ICollection<GetStudentRequestsDto>> MapToGetStudentRequestsDto(ICollection<Request> requests)
        {
            ICollection<GetStudentRequestsDto> result = [];
            foreach (Request request in requests)
            {
                request.Staff = await _repositories.StaffRepository.GetByIdAsync(request.StaffId);
                result.Add(new GetStudentRequestsDto()
                {
                    Title = request.Title,
                    Content = request.Content,
                    Date = request.Date,
                    RequestStatus = request.RequestStatus,
                    StaffId = request.StaffId,
                    StaffFullName = $"{request.Staff.FirstName} {request.Staff.MiddleName} {request.Staff.LastName}"
                });
            }

            return result;
        }
    }
}
