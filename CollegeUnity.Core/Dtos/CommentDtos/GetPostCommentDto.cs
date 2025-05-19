using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommentDtos
{
    public class GetPostCommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
        public EducationDegree? Degree{ get; set; }

        public string? ProfilePicturePath { get; set; }

        //public string DisplayedDateTime
        public DateTime DisplayedDateTime
        {
            get
            {
                //string datetimeFormat = "dd/MM/yyyy h:mm tt";
                //DateTime dateTime = EditedAt ?? CreatedAt;
                //return dateTime.ToString(datetimeFormat);
                DateTime dateTime = EditedAt ?? CreatedAt;
                return dateTime;
            }
        }

        private DateTime CreatedAt { get; set; }
        
        private DateTime? EditedAt { get; set; }

        public bool IsEdited => (EditedAt is not null);

        public string Content { get; set; }

        public GetPostCommentDto(int id, int userId, string? profilePicturePath, string userName, DateTime createdAt, DateTime? editedAt, string content, EducationDegree? degree)
        {
           
            Id = id;
            UserId = userId;
            ProfilePicturePath = profilePicturePath;
            UserName = userName;
            CreatedAt = createdAt;
            EditedAt = editedAt;
            Content = content;
        }

        public static GetPostCommentDto MapFrom(PostComment comment)
        {
            EducationDegree? userDegree = comment.User is Staff staff ?
                staff.EducationDegree:
                null;

            return new GetPostCommentDto(
                id: comment.Id,
                userId: comment.UserId,
                profilePicturePath: comment.User.ProfilePicturePath,
                degree: userDegree,
                userName: $"{comment.User.FirstName} {comment.User.LastName}",
                createdAt: comment.CreatedAt,
                editedAt: comment.EditedAt,
                content: comment.Content
                );
        }
    }
}
