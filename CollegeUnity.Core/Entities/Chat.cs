using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey(nameof(User1))]
        public required int User1Id { get; set; }

        [ForeignKey(nameof(User1Id))]
        public virtual User User1 { get; set; }

        [Required]
        [ForeignKey(nameof(User2))]
        public required int User2Id { get; set; }

        [ForeignKey(nameof(User2Id))]
        public virtual User User2 { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime LastMessageAt { get; set; }
        
        [Required]
        public required bool IsChattingEnabled { get; set; }
        public bool IsHiddenForUser1 { get; set; } = false;
        public bool IsHiddenForUser2 { get; set; } = false;


        public virtual ICollection<ChatMessage>? Messages { get; set; }

       
    }
}
