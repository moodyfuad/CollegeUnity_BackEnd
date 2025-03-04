using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.InterestedSubjectDtos
{
    public class GInterestedSubjectDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Major Major { get; set; }
        public required Level Level { get; set; }
        public required AcceptanceType AcceptanceType { get; set; }
        public required string TeacherName { get; set; }
    }
}
