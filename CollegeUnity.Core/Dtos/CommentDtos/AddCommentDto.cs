using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommentDtos
{
    public class AddCommentDto
    {
        [Required(ErrorMessage = "post id is required")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "user id is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "comment can not be empty")]
        public string Comment { get; set; }

        public AddCommentDto(int postId, int userId, string comment)
        {
            PostId = postId;
            UserId = userId;
            Comment = comment;
        }
    }
}
