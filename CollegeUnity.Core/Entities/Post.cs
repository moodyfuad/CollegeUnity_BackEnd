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
        [Required]
        public required int Id { get; set; }

        [Required]
        public required string Content { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }

        [Required]
        public required Priority Priority { get; set; }

        [Required]
        public required bool IsPublic { get; set; }

        [ForeignKey(nameof(Post))]
        [Required]
        public int StaffId { get; set; }
        [Required]
        public required Staff Staff { get; set; }

        public ICollection<PostFile>? PostFiles { get; set; }

        public ICollection<PostVote>? Votes { get; set; }

        public ICollection<PostComment>? Comments {  get; set; } 

        public Major? ForMajor { get; set; }

        public Level? ForLevel { get; set;}

        public AcceptanceType? ForAcceptanceType { get; set;}

        // For Subject 
    }
}
