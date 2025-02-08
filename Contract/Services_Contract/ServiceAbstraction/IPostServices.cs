

using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IPostServices
    {
        public Task CreatePublicPostAsync(CPublicPostDto dto);
        public Task<IEnumerable<Post>> GetPublicPostAsync(PostParameters postParameters);
        public Task<bool> CreateBatchPostAsync();
        public Task<bool> CreateSubjectPostAsync();
        public Task<bool> UpdateAsync();
        public Task<bool> DeleteAsync();
    }
}
