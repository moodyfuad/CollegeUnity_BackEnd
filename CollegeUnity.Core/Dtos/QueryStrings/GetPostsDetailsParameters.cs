using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetPostsDetailsParameters : QueryStringParameters
    {
        public DateTime? DateTime { get; set; }
    }
}
