using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.StaffFeatures
{
    public class GetStaffDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }
        public List<string> AcademicRoles { get; set; }
    }
}
