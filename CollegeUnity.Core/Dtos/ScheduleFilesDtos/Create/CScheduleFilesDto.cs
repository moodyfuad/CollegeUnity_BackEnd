using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.ScheduleFilesDtos.Create
{
    public class CScheduleFilesDto
    {
        [Required]
        [Url]
        public required string Path { get; set; }

        [Required]
        public required ScheduleTypes ScheduleType { get; set; }
        [Required]
        public required Major Major { get; set; }
        [Required]
        public required AcceptanceType AcceptanceType { get; set; }

        public Level? Level { get; set; }
    }
}
