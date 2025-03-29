using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.ScheduleFilesDtos.Get
{
    public class GScheduleFileDto
    {
        public required string Path { get; set; }
        public required ScheduleTypes ScheduleType { get; set; }
        public required Major Major { get; set; }
        public required AcceptanceType AcceptanceType { get; set; }
    }
}
