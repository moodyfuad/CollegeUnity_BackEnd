using CollegeUnity.Core.Dtos.PostDtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public interface IBasePost
    {
        public Task<bool> UpdatePostAsync(int postId, int staffId, UUpdatePostDto dto);
        public Task<bool> DeleteAsync(int staffId, int postId);
    }
}
