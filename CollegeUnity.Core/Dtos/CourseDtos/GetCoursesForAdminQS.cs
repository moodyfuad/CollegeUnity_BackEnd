using CollegeUnity.Core.Dtos.QueryStrings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class GetCoursesForAdminQS : QueryStringParameters
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool HideDeletedDetails { get; set; } = true;

        public bool IncludeStudents { get; set; } = false;


        private string? name;

        public string? Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return string.Empty;
                }
                else
                {
                    return name.Trim();
                }
            }
            set => name = value;
        }

        public string Description { get; set; } = string.Empty;

        public string LecturerName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Location { get; set; } = string.Empty;


    }
}
