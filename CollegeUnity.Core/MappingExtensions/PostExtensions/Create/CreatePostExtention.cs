using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostExtensions.Create
{
    public static class CreatePostExtention
    {
        // CPublicPostDto to Post
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

        // CPatchPostDto to Post
        public static Post ToPost<T>(this CBatchPostDto dto) where T : Post
        {
            return new Post
            {
                Content = dto.Content,
                CreatedAt = DateTime.Now,
                Priority = dto.Priority,
                ForAcceptanceType = dto.ForAcceptanceType,
                ForLevel = dto.ForLevel,
                ForMajor = dto.ForMajor,
                IsPublic = false,
                StaffId = dto.StaffId,
            };
        }
    }
}
