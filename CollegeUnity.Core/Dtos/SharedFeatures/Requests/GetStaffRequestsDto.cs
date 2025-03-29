using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Requests
{
    public class GetStaffRequestsDto : GetUserRequestsDto
    {
        public int StudentId { get; set; }
    }
}
