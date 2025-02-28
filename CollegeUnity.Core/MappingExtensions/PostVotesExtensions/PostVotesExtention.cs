using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostVotesExtensions
{
    public static class PostVotesExtention
    {
        public static PostVote ToPostVote<T>(this string vote, int postId) where T : PostVote
        {
            return new PostVote
            {
                Name = vote,
                PostId = postId,
            };
        }
    }
}
