using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetSubjectParameters : QueryStringParameters
    {
        public string? Name { get; set; }
        public Major? Major { get; set; }
        public Level? Level { get; set; }
        public AcceptanceType? AcceptanceType { get; set; }
    }
}
