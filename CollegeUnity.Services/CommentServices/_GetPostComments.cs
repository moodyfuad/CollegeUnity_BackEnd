using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.CommentServices
{
    public partial class CommentService
    {

        public async Task<PagedList<GetPostCommentDto>> _GetPostComments(GetPostCommentsParameters param)
        {
            PagedList<PostComment> comments = await _repositoryManager.CommentRepository.GetRangeByConditionsAsync(
                condition: c => c.PostId.Equals(param.PostId) && c.Status != CommentStatus.Deleted,
                queryStringParameters: param,
                includes: c => c.User);

            var result = await GetPostCommentDto.From<PagedList<PostComment>>(comments);
            return result;
        }
    }
}
