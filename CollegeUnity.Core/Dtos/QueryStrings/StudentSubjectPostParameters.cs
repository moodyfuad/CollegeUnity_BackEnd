using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class SubjectPostParameters : QueryStringParameters
    {
        public Level? Level { get; set; }
        public Major? Major { get; set; }
        public AcceptanceType? AcceptanceType { get; set; }
    }
}
