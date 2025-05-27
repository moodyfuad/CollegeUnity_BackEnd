using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Posts
{
    public interface IManagePostFeatures
    {
        Task<PagedList<GPostsByAdmin>> GetPostsDetails(GetPostsDetailsParameters parameters);
        Task<ResultDto> DeletePost(int postId);
    }
}
