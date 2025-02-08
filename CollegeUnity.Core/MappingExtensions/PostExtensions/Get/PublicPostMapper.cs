using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostExtensions.Get
{
    public static class PublicPostMapper
    {
        public static T ToPublicPostMapper<T>(this Post post) where T : PublicPostDto, new()
        {
            return new T
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                EditedAt = post.EditedAt,
                Priority = post.Priority,
                IsPublic = post.IsPublic,
                Staff = new()
                {
                    Id = post.StaffId,
                    Name = post.Staff.FirstName + " " + post.Staff.MiddleName + " " + post.Staff.LastName,
                    EducationDegree = post.Staff.EducationDegree
                },
                PostFiles = post.PostFiles?.Select(p => p.Path)
            };
        }

        public static IEnumerable<T> ToPublicPostMappers<T>(this IEnumerable<Post> posts) where T : PublicPostDto, new()
        {
            return posts.Select(post => post.ToPublicPostMapper<T>());
        }

    }
}
