using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetStaffParameters : QueryStringParameters
    {
        public string? FullName { get; set; }
        public Roles? Role { get; set; }
    }
}
