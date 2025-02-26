

using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IPostServices
    {
        //Done
        public Task CreatePublicPostAsync(CPublicPostDto dto);
        //Done
        public Task<IEnumerable<GPublicPostDto>> GetPublicPostAsync(PublicPostParameters postParameters);
        //Done
        public Task<IEnumerable<GBatchPostDto>> GetPublicAndBatchPostAsync(PublicAndBatchPostParameters batchPostParameters);
        //Done
        public Task CreateBatchPostAsync(CBatchPostDto dto);
        //Done
        public Task CreateSubjectPostAsync(CSubjectPostDto dto);
        //Done
        public Task<IEnumerable<GStudentBatchPost>> GetSubjectPostsForStudent(SubjectPostParameters parameters);
        public Task<bool> UpdateAsync();
        //Done
        public Task<bool> DeleteAsync();
    }
}
