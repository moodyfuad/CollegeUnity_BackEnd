using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommentDtos
{
    public class DeleteCommentResultDto
    {
        public bool IsSuccess { get; }

        public string Message { get; }


        private DeleteCommentResultDto(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static DeleteCommentResultDto Success(string? msg = null)
        {
            msg ??= "Comment Deleted";
            return new DeleteCommentResultDto(true, msg);
        }
        
        public static DeleteCommentResultDto Failed(string errorMessage)
        {
            string msg = errorMessage;
            return new DeleteCommentResultDto(true, msg);
        }

    }
}
