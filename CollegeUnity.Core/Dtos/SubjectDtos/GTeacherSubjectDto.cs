using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SubjectDtos
{
    public class GTeacherSubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public Major Major { get; set; }
        public AcceptanceType AcceptanceType { get; set; }
    }
}
