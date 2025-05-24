using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public interface IManageBatchPostsFeatures : IBasePost
    {
        // Add, Delete, Edit
        Task<ResultDto> CreateBatchPostAsync(CBatchPostDto dto, int staffId);
    }
}
