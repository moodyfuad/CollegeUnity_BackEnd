using CollegeUnity.Core.Dtos.PostDtos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public interface IManageSubjectPostsFeatures
    {
        // C, U, D
        public Task CreateSubjectPostAsync(CSubjectPostDto dto);
    }
}
