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
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Content { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }

        [Required]
        public required Priority Priority { get; set; }

        [Required]
        public required bool IsPublic { get; set; }

        [ForeignKey(nameof(Staff))]
        [Required]
        public int StaffId { get; set; }
        [Required]
        public virtual Staff Staff { get; set; }

        public virtual ICollection<PostFile>? PostFiles { get; set; }

        public virtual ICollection<PostVote>? Votes { get; set; }

        public virtual ICollection<PostComment>? Comments {  get; set; } 

        public Major? ForMajor { get; set; }

        public Level? ForLevel { get; set;}

        public AcceptanceType? ForAcceptanceType { get; set;}

        // For Subject
        [ForeignKey(nameof(Subject))]
        public int? SubjectId {get; set;}
        public Subject? Subject{ get; set; }
    }
}
