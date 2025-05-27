using CollegeUnity.Contract.AdminFeatures.Posts;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.Posts
{
    public class ManagePostFeatures : IManagePostFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public ManagePostFeatures(IRepositoryManager repositoryManager) 
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GPostsByAdmin>> GetPostsDetails(GetPostsDetailsParameters parameters)
        {
            Expression<Func<Post, bool>> condition =
                d => d.CreatedAt.Date == parameters.DateTime.Date ||
                (d.EditedAt != null && d.EditedAt.Value.Date == parameters.DateTime.Date);
            var posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(condition, parameters);
            return posts.MapPagedList(GetPostExtention.ToGetPost);
        }

        public async Task<ResultDto> DeletePost(int postId)
        {
            var post = await _repositoryManager.PostRepository.GetByIdAsync(postId);
            if (post is null)
                return new(false, "Post not found.");
            post.Delete();
            await _repositoryManager.PostRepository.Update(post);
            await _repositoryManager.SaveChangesAsync();
            return new(true);
        }
    }
}
