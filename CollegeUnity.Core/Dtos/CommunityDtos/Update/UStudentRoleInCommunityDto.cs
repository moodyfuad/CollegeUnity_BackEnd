using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommunityDtos.Update
{
    public class UStudentRoleInCommunityDto
    {
        [Required(ErrorMessage = "student is required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "community is required")]
        public int CommunityId { get; set; }
        [Required(ErrorMessage = "role is required")]
        public CommunityMemberRoles Role { get; set; } 
    }
}
