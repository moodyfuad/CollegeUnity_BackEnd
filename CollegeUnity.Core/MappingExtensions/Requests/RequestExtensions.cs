using CollegeUnity.Core.Dtos.SharedFeatures.Requests;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollegeUnity.Core.MappingExtensions.Requests
{
    public static class RequestExtensions
    {

        public static void MapTo(
            this PagedList<Request> requests,
            out PagedList<GetUserRequestsDto> result)
        {
            List<GetUserRequestsDto> items = [];
            foreach (var request in requests)
            {
                request.MapTo(out GetUserRequestsDto item);
                items.Add(item);
            }

            var pagedList = new PagedList<GetUserRequestsDto>(
                items: items,
                count: requests.TotalCount,
                pageNumber: requests.CurrentPage,
                pageSize: requests.PageSize);

            result = pagedList;
        }

        public static void MapTo(this Request request, out GetUserRequestsDto result)
        {
            string eduDegree = request.Staff.EducationDegree.ToString();
             result = new()
            {
                RequestId = request.Id,
                StudentId = request.StudentId,
                StaffId = request.StaffId,
                StaffFullName = request.Staff != null ? $"{eduDegree}. {request.Staff.FirstName} {request.Staff.MiddleName} {request.Staff.LastName}" : "staff is null",

                StudentFullName = request.Student != null ? $"{request.Student.FirstName} {request.Student.MiddleName} {request.Student.LastName}" : "Student is null",

                Title = request.Title,
                Content = request.Content,
                Date = request.Date,
                RequestStatus = request.RequestStatus
            };
        }

    }
}
