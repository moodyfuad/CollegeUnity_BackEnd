using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Posts.Comments;
using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.CommentExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Posts.Comments
{
    public class CommentFeatures(IRepositoryManager _repositories) : ICommentFeatures
    {
        public async Task<AddCommentResultDto> AddComment(AddCommentDto dto)
        {
            var post = await _repositories.PostRepository.GetByIdAsync(dto.PostId);
            var user = await _repositories.UserRepository.GetByIdAsync(dto.UserId);
            if (post == null)
            {
                string errorMessage = "NO Post Found";
                return AddCommentResultDto.Failed(errorMessage);
            }

            if (user == null)
            {
                string errorMessage = "NO User Found";
                return AddCommentResultDto.Failed(errorMessage);
            }

            if (string.IsNullOrEmpty(dto.Comment) || string.IsNullOrWhiteSpace(dto.Comment))
            {
                string errorMessage = "Empty Comment is not allowed";
                return AddCommentResultDto.Failed(errorMessage);
            }

            var comment = dto.To<PostComment>();
            comment = await _repositories.CommentRepository.CreateAsync(comment);
            await _repositories.SaveChangesAsync();

            var result =
                comment == null ?
                AddCommentResultDto.Failed("Comment Not Added") :
                AddCommentResultDto.Success(comment.Content);

            return result;
        }

        public async Task<DeleteCommentResultDto> DeleteComment(int commentId)
        {
            bool isExist = await IsExist(commentId);
            if (!isExist)
            {
                return DeleteCommentResultDto.Failed($"No Comment With [id = {commentId}]");
            }

            PostComment? comment = await _repositories.CommentRepository.GetByIdAsync(commentId);

            if (comment.Status == CommentStatus.Deleted)
            {
                return DeleteCommentResultDto.Failed($"Comment Already Deleted");
            }

            comment.Status = CommentStatus.Deleted;
            comment = await _repositories.CommentRepository.Update(comment);
            await _repositories.SaveChangesAsync();

            if (comment != null)
            {
                return DeleteCommentResultDto.Success();
            }

            return DeleteCommentResultDto.Failed("An Error occurs while deleting, please try again");
        }

        public async Task<EditCommentDto> EditComment(EditCommentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.newComment))
            {
                return EditCommentDto.Failed(dto.id, "can not post empty comment.");
            }

            PostComment? comment = await _repositories.CommentRepository.GetByIdAsync(dto.id);
            bool exist = comment != null;
            if (!exist || comment?.Status == CommentStatus.Deleted)
            {
                return EditCommentDto.Failed(dto.id, "can not find this comment, it may be deleted");
            }

            comment!.Content = dto.newComment;
            comment.EditedAt = DateTime.UtcNow.ToLocalTime();
            comment = await _repositories.CommentRepository.Update(comment);

            await _repositories.SaveChangesAsync();
            if (comment is null)
            {
                return EditCommentDto.Failed(dto.id, "can not update your comment right now, please try again later");
            }

            return EditCommentDto.Success(dto.id, "updated");
        }

        public async Task<PagedList<GetPostCommentDto>> GetPostComments(GetPostCommentsParameters param)
        {
            PagedList<PostComment> comments = await _repositories.CommentRepository.GetRangeByConditionsAsync(
                condition: c =>
                    c.PostId.Equals(param.PostId) &&
                    c.Status == CommentStatus.Published,
                queryStringParameters: param,
                includes: c => c.User);

            var result = await GetPostCommentDto.From<PagedList<PostComment>>(comments);
            return result;
        }

        private async Task<bool> IsExist(int id)
        {
            var result = await _repositories.CommentRepository.GetByIdAsync(id);
            return result != null;
        }
    }
}
