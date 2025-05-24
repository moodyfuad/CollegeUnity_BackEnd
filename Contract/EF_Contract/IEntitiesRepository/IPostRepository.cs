
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System.Linq.Expressions;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<bool> isMyPost(int staffId, int postId);
        Task<bool> isExist(int postId);
        Task<PagedList<Post>> GetVotesWithConditionsAsync(
    Expression<Func<Post, bool>> condition,
    QueryStringParameters queryParams,
    bool trackChanges = false,
    params Expression<Func<Post, object>>[] includes);
    }
}
