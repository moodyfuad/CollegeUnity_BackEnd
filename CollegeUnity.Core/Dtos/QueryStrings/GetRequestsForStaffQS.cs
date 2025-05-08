using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetRequestsForStaffQS : QueryStringParameters
    {
        public string? StudentName { get; set; }
    }
}
