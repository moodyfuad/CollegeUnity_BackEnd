using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Posts.Comments
{
     public interface ICommentFeatures
    {
        Task<AddCommentResultDto> AddComment(int userId, int postId, AddCommentDto addCommentDto);

        Task<PagedList<GetPostCommentDto>> GetPostComments(int postId, GetPostCommentsParameters param);

        Task<EditCommentDto> EditComment(int userId, EditCommentDto editCommentDto);

        Task<DeleteCommentResultDto> DeleteComment(int userId, int commentId);
    }
}
