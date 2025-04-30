using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.FeedBackDtos.Create
{
    public class UFeedBackResponseDto
    {
        public string Response { get; set; }
        public enFeedBackStatus Status { get; set; }
    }
}
