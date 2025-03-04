using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Post
{
    public interface IGetSubjectPostFeatures
    {
        Task<IEnumerable<GSubjectPostDto>> GetSubjectPosts(GetSubjectPostParameters parameters);
    }
}
