using CollegeUnity.Core.Dtos.PostDtos.Update;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostExtensions.Update
{
    public static class UpdatePostExtention
    {
        public static Post GetPostUpdated(this Post post, UUpdatePostDto dto)
        {
            post.EditedAt = DateTime.Now;
            post.Content = dto.Content;
            return post;
        }
    }
}
