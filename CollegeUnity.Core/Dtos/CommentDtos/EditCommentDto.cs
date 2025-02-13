using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommentDtos
{
    public class EditCommentDto
    {
        public int id { get; set; }

        public string newComment { get; set; }

        public string? Message { get; }

        public bool? IsSuccess { get;  }

        private EditCommentDto(int id, string Message, bool isSuccess)
        {
            this.id = id;
            this.Message = Message;
            IsSuccess = isSuccess;
        }

        public EditCommentDto(int id, string newComment)
        {
            this.id = id;
            this.newComment = newComment;
        }

        public static EditCommentDto Success(int id,string Message)
        {
            return new(id, Message, true);
        }
        
        public static EditCommentDto Failed(int id,string Message)
        {
            return new(id, Message, false);
        }

    }
}
