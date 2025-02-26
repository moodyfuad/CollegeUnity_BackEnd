using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public interface IDeletePost
    {
        public Task<bool> DeleteAsync(int staffId, int postId);
    }
}
