using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Requests
{
    public class GetStudentRequestsDto : GetUserRequestsDto
    {
        public int StaffId { get; set; }
    }
}
