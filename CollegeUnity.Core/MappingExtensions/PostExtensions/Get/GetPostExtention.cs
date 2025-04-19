using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostExtensions.Get
{
    public static class GetPostExtention
    {
        /// <summary>
        /// this method will map any type of getting post to send it to client
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="post"></param>
        /// <returns></returns>
        #region Public post
        public static T ToGPostDto<T>(this Post post) where T : GPostDto, new()
        {
            var dto = new T
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                EditedAt = post.EditedAt,
                Staff = new GPostDto.StaffInfo
                {
                    Id = post.StaffId,
                    Name = $"{post.Staff.FirstName} {post.Staff.MiddleName} {post.Staff.LastName}",
                    EducationDegree = post.Staff.EducationDegree,
                    ProfilePicPath = post.Staff.ProfilePicturePath,
                    TeachNames = post.Staff.Roles.Select(r => r.AsString())
                },
                PostFiles = post.PostFiles?.Select(p => p.Path).ToList(),
                //Votes = post.Votes?.Select(p => p.Name).ToList()
            };

            if (dto is GPublicPostDto publicPostDto)
            {
                publicPostDto.Priority = post.Priority;
                publicPostDto.IsPublic = post.IsPublic;
            }

            if (dto is GBatchPostDto batchPostDto)
            {
                batchPostDto.IsPublic = post.IsPublic;
            }

            if (dto is GStudentBatchPost studentBatchPostDto)
            {
                studentBatchPostDto.SubjectName = post.Subject.Name;
            }

            if (dto is GSubjectPostDto subjectPostDto)
            {
                subjectPostDto.SubjectName = post.Subject.Name;
            }

            return dto;
        }
        public static PagedList<T> ToGPostMappers<T>(this PagedList<Post> posts) where T : GPostDto, new()
        {
            var results = posts.Select(post => post.ToGPostDto<T>()).ToList();
            var pagedList = new PagedList<T>
                (
                    items: results,
                    count: results.Count(),
                    pageNumber: posts.CurrentPage,
                    pageSize: posts.PageSize
                );
            return pagedList;
        }
        #endregion

    }
}
