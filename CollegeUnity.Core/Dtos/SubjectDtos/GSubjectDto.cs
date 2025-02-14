using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SubjectDtos
{
    public class GSubjectDto : SubjectDto
    {
        public string? TeacherName { get; set; }
        public string? AssignedByName { get; set; }
    }
}
