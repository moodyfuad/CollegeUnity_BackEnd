using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Posts.Comments;
using CollegeUnity.Core.CustomExceptions;
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
        public async Task<AddCommentResultDto> AddComment(int userId, int postId, AddCommentDto dto)
        {
            var post = await _repositories.PostRepository.GetByIdAsync(postId);
            var user = await _repositories.UserRepository.GetByIdAsync(userId);
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

            PostComment comment = new()
            {
                Content = dto.Comment,
                Post = default,
                PostId = postId,
                User = default,
                UserId = userId,
                CreatedAt = DateTime.Now.ToLocalTime(),
            };
            comment = await _repositories.CommentRepository.CreateAsync(comment);
            await _repositories.SaveChangesAsync();

            var result =
                comment == null ?
                AddCommentResultDto.Failed("Comment Not Added") :
                AddCommentResultDto.Success(comment.Content);

            return result;
        }

        public async Task<DeleteCommentResultDto> DeleteComment(int userId, int commentId)
        {
            var user = await _repositories.UserRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new UnauthorizedAccessException($"User with given Not Found");
            }

            bool isExist = await IsExist(commentId);
            if (!isExist)
            {
                return DeleteCommentResultDto.Failed($"No Comment With [id = {commentId}]");
            }

            PostComment? comment = await _repositories.CommentRepository.GetByConditionsAsync(
                condition: c => c.Id.Equals(commentId),
                includes: c => c.Post);

            // not the comment owner or post owner
            if (comment.UserId != userId && comment.Post.StaffId != userId)
            {
                throw new ForbiddenException("Action Blocked", ["The Given user is not the comment publisher"]);
            }

            if (comment.Status == CommentStatus.Deleted)
            {
                return DeleteCommentResultDto.Failed($"Comment Already Deleted");
            }

            comment.Status = CommentStatus.Deleted;
            try
            {
                await _repositories.SaveChangesAsync();
                return DeleteCommentResultDto.Success();
            }
            catch (Exception e)
            {
                throw new InternalServerException("An Error occurs while deleting, please try again", [e.Message]);
            }
        }

        public async Task<EditCommentDto> EditComment(int userId, EditCommentDto dto)
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

            if (comment.UserId != userId)
            {
                throw new ForbiddenException("Action Blocked", ["The Given user is not the comment publisher"]);
            }

            comment!.Content = dto.newComment;
            comment.EditedAt = DateTime.UtcNow.ToLocalTime();
            //comment = await _repositories.CommentRepository.Update(comment);

            try
            {
                await _repositories.SaveChangesAsync();
                return EditCommentDto.Success(dto.id, "updated");
            }
            catch (Exception e)
            {
                throw new InternalServerException("can not update your comment right now, please try again later", [e.Message]);
            }
        }

        public async Task<PagedList<GetPostCommentDto>> GetPostComments(int postId ,GetPostCommentsParameters param)
        {
            PagedList<PostComment> comments = await _repositories.CommentRepository.GetRangeByConditionsAsync(
                condition: c =>
                    c.PostId.Equals(postId) &&
                    c.Status != CommentStatus.Deleted,
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
