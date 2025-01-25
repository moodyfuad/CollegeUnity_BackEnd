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
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Chat))]
        public int ChatId { get; set; }
        [Required]
        public Chat Chat { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public required User MessageSender {  get; set; }


        public required User Receiver {  get; set; }
        public required string Message { get; set; }
        public required MessageStatus Status { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public DateTime EditedAt { get; set; }
    }
}
