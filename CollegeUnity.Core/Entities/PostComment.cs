using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public CommentStatus Status { get; set; } = CommentStatus.Published;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }
        

        [ForeignKey(nameof(User))]
        [Required]
        public int UserId { get; set; }
        [Required]
        public virtual required User User { get; set; }

        [ForeignKey(nameof(Post))]
        [Required]
        public int PostId { get; set; }

        public virtual required Post Post { get; set; }

       
    }
}
