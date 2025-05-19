using CollegeUnity.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int ChatId { get; set; }
        [Required]

        [ForeignKey(nameof(ChatId))]
        public virtual Chat Chat { get; set; }

        public required int SenderId {  get; set; }

        [Required]
        [ForeignKey(nameof(SenderId))]
        public virtual User Sender { get; set; }
        
        [Required]
        public required int RecipientId { get; set; }

        [ForeignKey(nameof(RecipientId))]
        
        public virtual User Recipient { get; set; }
        [Required]
        public required string Content { get; set; }

        [Required]
        public required MessageStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime EditedAt { get; set; }

    }
}
