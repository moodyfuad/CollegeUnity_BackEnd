using CollegeUnity.Core.Dtos.QueryStrings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class GetCoursesForStudentQS : QueryStringParameters
    {
        [DisplayName("Course Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Get My Courses Only")]
        public bool IsMyCourses { get; set; } = false;
    }
}
