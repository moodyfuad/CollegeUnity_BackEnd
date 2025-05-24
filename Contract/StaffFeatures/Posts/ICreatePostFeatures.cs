using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public interface ICreatePostFeatures
    {
        Task<ResultDto> CreatePostAsync<TDto>(TDto dto, int staffId) where TDto : CPostDto;
    }
}
