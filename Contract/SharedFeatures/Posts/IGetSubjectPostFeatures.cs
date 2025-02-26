using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Posts
{
     public interface IGetSubjectPostFeatures
    {
        // get
        public Task<IEnumerable<GStudentBatchPost>> GetSubjectPosts(SubjectPostParameters parameters);
    }
}
