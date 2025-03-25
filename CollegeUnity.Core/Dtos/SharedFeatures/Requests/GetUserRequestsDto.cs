using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Requests
{
    public class GetUserRequestsDto
    {
        public int RequestId { get; set; }

        public int StudentId { get; set; }

        public string StudentFullName { get; set; }

        public int StaffId { get; set; }

        public string StaffFullName { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public RequestStatus RequestStatus { get; set; }


    }
}
