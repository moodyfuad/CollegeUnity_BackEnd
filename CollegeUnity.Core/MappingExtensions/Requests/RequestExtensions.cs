using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.Requests
{
    public static class RequestExtensions
    {

        public static void MapTo(
             this PagedList<Request> requests,
             out PagedList<GetStudentRequestsDto> result)
        {
            List<GetStudentRequestsDto> items = [];

            requests.ForEach(request => items.Add(new GetStudentRequestsDto()
            {
                RequestId = request.Id,
                Title = request.Title,
                Content = request.Content,
                Date = request.Date,
                RequestStatus = request.RequestStatus,
                StaffId = request.StaffId,
                StaffFullName = 
                    request.Staff != null ?
                    $"{request.Staff.FirstName} {request.Staff.MiddleName} {request.Staff.LastName}" :
                    "staff is null"
            }));
            var pagedList = new PagedList<GetStudentRequestsDto>(
                items: items,
                count: requests.TotalCount,
                pageNumber: requests.CurrentPage,
                pageSize: requests.PageSize);

            result = pagedList;
        }

        public static void MapMapGetStudentRequestsDto(this Request request, out GetStudentRequestsDto result)
        {
            string staffFullName = "staff is null";
            if (request.Staff != null)
            {
                staffFullName = $"{request.Staff.FirstName} {request.Staff.MiddleName} {request.Staff.LastName}";
            }

            result = new GetStudentRequestsDto()
            {
                Title = request.Title,
                Content = request.Content,
                Date = request.Date,
                RequestStatus = request.RequestStatus,
                StaffId = request.StaffId,
                StaffFullName = staffFullName
            };
        }

    }
}
