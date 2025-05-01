using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Request;
using CollegeUnity.Core.CustomExceptions;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Requests
{
    public class StaffRequestsFeatures : IStaffRequestsFeatures
    {
        private readonly IRepositoryManager _repositories;

        public StaffRequestsFeatures(IRepositoryManager repositories)
        {
            _repositories = repositories;
        }

        public async Task<ApiResponse<string?>> ChangeRequestStatus(int staffId, int requestId, RequestStatus newStatus)
        {
            var request = await _repositories.RequestRepository.GetByConditionsAsync(
                condition: request => request.Id.Equals(requestId) && request.StaffId == staffId,
                trackChanges: true) ??
                throw new KeyNotFoundException("Request Not Found");
            request.RequestStatus = newStatus;

            await _repositories.SaveChangesAsync();

            return ApiResponse<string?>.Success(null, $"Request with title :'{request.Title}' is {request.RequestStatus.ToString()} Successfully.");
        }

        public async Task<ApiResponse<PagedList<GetUserRequestsDto>>>
            GetStudentsRequestsAsync(int staffId, GetRequestsForStaffQS queryString)
        {
            List<string> fullName = queryString.StudentName?.Split(' ').ToList() ?? [];
            string firstName = fullName.Count > 0 ? fullName[0] : string.Empty;
            string middleName = fullName.Count > 1 ? fullName[1] : string.Empty;
            string lastName = fullName.Count > 2 ? fullName[2] : string.Empty;

            var requests = await _repositories.RequestRepository.GetRangeByConditionsAsync(
                condition: request =>
                    request.StaffId == staffId &&
                    request.Student.FirstName.Contains(firstName) &&
                    request.Student.MiddleName.Contains(middleName) &&
                    request.Student.LastName.Contains(lastName),
                queryStringParameters: queryString,
                includes: [req => req.Staff, req => req.Student]);

            var convertedPagesList = requests.To(RequestExtensions.MappingFun);

             return ApiResponse<PagedList<GetUserRequestsDto>>.Success(convertedPagesList);
        }
    }
}
