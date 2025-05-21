using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class StudentCommunity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        [Required]
        [ForeignKey(nameof(Community))]
        public int CommunityId { get; set; }

        public virtual Community Community { get; set; }
        [Required]
        public required CommunityMemberRoles Role {  get; set; }
        public DateTime? LastSeen { get; set; }

        public virtual ICollection<CommunityMessage>? CommunityMessages { get; set; }
    }
}
