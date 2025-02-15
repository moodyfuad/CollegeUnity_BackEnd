using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.CommentServices
{
    public partial class CommentService
    {
        private async Task<DeleteCommentResultDto> _DeleteComment(int commentId)
        {
             bool isExist = await IsExist(commentId);
            if (!isExist)
            {
                return DeleteCommentResultDto.Failed($"No Comment With [id = {commentId}]");
            }

            PostComment? comment = await _repositoryManager.CommentRepository.GetByIdAsync(commentId);

            if (comment.Status == CommentStatus.Deleted)
            {
                return DeleteCommentResultDto.Failed($"Comment Already Deleted");
            }
            else
            {
                comment.Status = CommentStatus.Deleted;
                comment = await _repositoryManager.CommentRepository.Update(comment);
                await _repositoryManager.SaveChangesAsync();
                if (comment != null)
                {
                    return DeleteCommentResultDto.Success();
                }

                return DeleteCommentResultDto.Failed("An Error occurs while deleting, please try again");
            }
        }
    }
}
