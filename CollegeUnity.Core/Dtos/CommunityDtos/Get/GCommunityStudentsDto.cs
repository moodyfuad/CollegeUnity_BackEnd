using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommunityDtos.Get
{
    public class GCommunityStudentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CommunityMemberRoles Role { get; set; }
    }
}
