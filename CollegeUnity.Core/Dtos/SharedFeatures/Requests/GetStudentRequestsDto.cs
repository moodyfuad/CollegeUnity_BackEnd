using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Requests
{
    public class GetStudentRequestsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public int StaffId { get; set; }

        public string StaffFullName { get; set; }
    }
}
