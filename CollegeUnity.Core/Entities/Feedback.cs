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
    public class Feedback
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Location { get; set; }
        [Required]
        public enFeedBackStatus FeedBackStatus { get; set; } = enFeedBackStatus.New;
        public enTypeOfFeedBack TypeOfFeedBack { get; set; }
        public string? Response { get; set; }
        [Required]
        [ForeignKey(nameof(FromUser))]
        public int UserId { get; set; }
        public virtual User FromUser { get; set; }
    }
}
