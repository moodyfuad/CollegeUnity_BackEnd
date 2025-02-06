

using CollegeUnity.Core.Dtos.PostDtos.Create;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IPostServices
    {
        public Task CreatePublicPostAsync(CPublicPostDto dto);
        public Task<bool> CreateBatchPostAsync();
        public Task<bool> CreateSubjectPostAsync();
        public Task<bool> UpdateAsync();
        public Task<bool> DeleteAsync();
    }
}
