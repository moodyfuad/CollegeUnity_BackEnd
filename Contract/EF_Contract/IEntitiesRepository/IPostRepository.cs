
using CollegeUnity.Core.Entities;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<bool> isMyPost(int staffId, int postId);
        Task<bool> isExist(int postId);
    }
}
