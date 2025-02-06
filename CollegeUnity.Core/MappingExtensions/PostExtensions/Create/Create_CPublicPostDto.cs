using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostExtensions.Create
{
    public static partial class CreatePostExtention
    {

        public static Post ToPost<T>(this CPublicPostDto dto) where T : Post
        {
            return new Post
            {
                Content = dto.Content,
                CreatedAt = DateTime.Now,                
                Priority = dto.Priority,
                IsPublic = true,
                StaffId = dto.StaffId,
            };
        }
    }
}
