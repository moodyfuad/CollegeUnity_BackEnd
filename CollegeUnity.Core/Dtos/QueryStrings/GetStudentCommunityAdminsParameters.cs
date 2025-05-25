using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetStudentCommunityAdminsParameters : QueryStringParameters
    {
        public int communityId { get; set; }
        public bool Admins { get; set; }
    }
}
