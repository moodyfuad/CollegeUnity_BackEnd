using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Post
{
    public interface IGetSubjectPostFeatures
    {
        Task<PagedList<GSubjectPostDto>> GetSubjectPosts(int userId, GetSubjectPostParameters parameters);
    }
}
