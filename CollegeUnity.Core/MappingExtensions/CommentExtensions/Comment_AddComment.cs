using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommentExtensions
{
    public static class Comment_AddComment
    {
        public static PostComment To<T>(this AddCommentDto dto, Post post,User user) where T : PostComment
        {
            return new PostComment()
            {
                Content = dto.Comment,
                Post = post,
                PostId = dto.PostId,
                User = user,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow.ToLocalTime()
            };
        }
    }
}
