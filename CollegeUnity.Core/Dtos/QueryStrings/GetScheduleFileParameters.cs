using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetScheduleFileParameters : QueryStringParameters
    {
        [Required]
        public required Major Major { get; set; }
        [Required]
        public required AcceptanceType AcceptanceType { get; set; }
        public ScheduleTypes? ScheduleTypes { get; set; }
    }
}
