using CollegeUnity.Core.Dtos.QueryStrings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class GetCoursesForAdminQS : QueryStringParameters
    {
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string LecturerName { get; set; } = "";

        public DateTime Date { get; set; }

        public string Location { get; set; } = "";
    }
}
