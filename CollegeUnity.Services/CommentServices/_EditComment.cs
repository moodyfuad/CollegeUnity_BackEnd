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
        private async Task<EditCommentDto> _EditComment(EditCommentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.newComment))
            {
                return EditCommentDto.Failed(dto.id, "can not post empty comment.");
            }

            bool exist = await IsExist(dto.id);
            if (!exist)
            {
                return EditCommentDto.Failed(dto.id, "can not find this comment, it may be deleted");
            }

            PostComment comment = await _repositoryManager.CommentRepository.GetByIdAsync(dto.id);
            comment.Content = dto.newComment;
            comment.EditedAt = DateTime.UtcNow.ToLocalTime();
            comment = await _repositoryManager.CommentRepository.Update(comment);

            await _repositoryManager.SaveChangesAsync();
            if (comment is null)
            {
                return EditCommentDto.Failed(dto.id, "can not update your comment right now, please try again later");
            }

            return EditCommentDto.Success(dto.id, "updated");
        }
    }
}
