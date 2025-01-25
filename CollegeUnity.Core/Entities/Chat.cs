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


        [ForeignKey(nameof(Staff))]
        [Required]
        public int CreatorId { get; set; }
        [Required]
        [InverseProperty(nameof(Staff.CreatedChats))]
        public required Staff ChatCreator { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public int ChatReceiverId { get; set; }
        [Required]
        [InverseProperty(nameof(User.ReceivedChats))]
        public required User ChatReceiver { get; set; }

        public ICollection<ChatMessage>? Messages { get; set; }

        [Required]
        public required bool IsChattingEnabled { get; set; }
       
    }
}
