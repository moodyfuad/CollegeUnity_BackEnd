using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class Community
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }

        [Required]
        public required CommunityType CommunityType { get; set; }
        [Required]
        public required CommunityState CommunityState { get; set; }

        public virtual ICollection<StudentCommunity>? CommunityStudents { get; set; }

        public virtual ICollection<CommunityMessage>? CommunityMessages { get; set; }

    }
}
