using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.StudentCommunityDtos.Get
{
    public class GCommunityAdminsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? Role { get; set; }
    }
}
