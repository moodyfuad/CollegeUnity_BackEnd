using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommentDtos
{
    public class AddCommentResultDto
    {
        public bool IsSuccess { get; } = false;

        public string Message { get; }

        public string Comment { get; }

        public int PostId{ get; }

        private Post? Post{ get; }


        public int UserId{ get; }

        public User? User { get; }

        private AddCommentResultDto(string comment, int postId, int userId)
        {
            Comment = comment;
            PostId = postId;
            UserId = userId;
        }

        private AddCommentResultDto(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            Message = message;
        }
        private AddCommentResultDto(bool isSuccess, string message, string comment)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Comment = comment;
        }
        public static AddCommentResultDto Create(string comment,int postId, int userId)
        {
            return new(comment,postId,userId);
        }

        public static AddCommentResultDto Success(string comment)
        {
            return new(true, "Comment Added", comment);
        }

        
        
        public static AddCommentResultDto Failed(string message)
        {
            return new(false, message);
        }
    }
}
