

using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IPostServices
    {
        public Task CreatePublicPostAsync(CPublicPostDto dto);
        public Task<IEnumerable<PublicPostDto>> GetPublicPostAsync(PostParameters postParameters);
        public Task CreateBatchPostAsync(CBatchPostDto dto);
        public Task<bool> CreateSubjectPostAsync();
        public Task<bool> UpdateAsync();
        public Task<bool> DeleteAsync();
    }
}
