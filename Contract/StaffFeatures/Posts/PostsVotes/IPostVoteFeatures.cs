using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes
{
    public interface IPostVoteFeatures
    {
        Task<bool> AddVotesToPost(ICollection<string> votes, int postId);
    }
}
