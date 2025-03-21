using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetCommunitesParameters : QueryStringParameters
    {
        public string? Name { get; set; }
        public CommunityType? CommunityType { get; set; }
        public CommunityState? CommunityState { get; set; }
    }
}
