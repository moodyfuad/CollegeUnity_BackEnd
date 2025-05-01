using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Request
{
    public interface IStaffRequestsFeatures
    {
        Task<ApiResponse<PagedList<GetUserRequestsDto>>> GetStudentsRequestsAsync(int staffId, GetRequestsForStaffQS queryString);

        Task<ApiResponse<string?>> ChangeRequestStatus(int staffId, int requestId, RequestStatus newStatus);

        // todo: hide => edit Staff_Request Table +studentStatus, +StaffStatus = visible, hidden
    }
}
