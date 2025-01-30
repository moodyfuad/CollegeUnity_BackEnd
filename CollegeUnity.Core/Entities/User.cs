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
    public class User
    {
        [Key]
        public  int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string MiddleName { get; set; }
        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [Phone]
        public required string Phone { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Required]
        public required string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }

        public required DateOnly BirthDate { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }
        [Required]
        public required AccountStatus AccountStatus { get; set; }
        public string? AccountStatusReason { get; set; }
        [Url]
        public string? ProfilePicturePath { get; set; }

        public string? VerificationCode { get; set; }

        public virtual ICollection<PostVote>? Votes { get; set; }

        public virtual ICollection<Feedback>? Feedbacks { get; set; }

        public virtual ICollection<Chat>? Chats { get; set; }

        public virtual ICollection<ChatMessage>? ChatMessages { get; set; }
        
        
        //[InverseProperty(nameof(ChatMessage.MessageSender))]
        //public ICollection<ChatMessage>?  { get; set; }
        
        


    }
}
