using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SubjectDtos
{
    public class CSubjectDto
    {
        [Required(ErrorMessage = "Name is requied")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Major is requied")]
        public required Major Major { get; set; }
        [Required(ErrorMessage = "Level is requied")]
        public required Level Level { get; set; }
        [Required(ErrorMessage = "Acceptance Type is requied")]
        public required AcceptanceType AcceptanceType { get; set; }
        public int? TeacherId { get; set; }
        public int? HeadOfScientificDepartmentId { get; set; }
    }

}
