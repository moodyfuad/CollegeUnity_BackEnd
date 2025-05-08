using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetStudentParameters : QueryStringParameters
    {
        public string? NameOrCardId { get; set; }
        public Level? Level { get; set; }
        public AccountStatus? AccountStatus { get; set; }
    }
}
