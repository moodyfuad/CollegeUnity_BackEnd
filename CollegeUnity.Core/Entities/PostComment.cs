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

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }
        

        [ForeignKey(nameof(Entities.User))]
        [Required]
        public int UserId { get; set; }
        [Required]
        public required User User { get; set; }

        [ForeignKey(nameof(Post))]
        [Required]
        public int PostId { get; set; }

       
    }
}
