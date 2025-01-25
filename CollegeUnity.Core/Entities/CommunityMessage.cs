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
    public class CommunityMessage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(StudentCommunity))]
        public int StudentCommunityId { get; set; }
        [Required]
        public virtual required StudentCommunity StudentCommunity { get; set; }

        [Required]
        [ForeignKey(nameof(Community))]
        public int CommunityId { get; set; }

        public virtual required Community Community { get; set; }


        public CommunityMemberRoles Role  { get => StudentCommunity.Role; }
        [Required]
        public required string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? EditedAt { get; set; }


        public bool IsDeleted { get; set; } = false;

    }
}
