using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
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

        public GetPostCommentDto(int id, int userId, string userName, DateTime createdAt, DateTime? editedAt, string content)
        {
           
            Id = id;
            UserId = userId;
            UserName = userName;
            CreatedAt = createdAt;
            EditedAt = editedAt;
            Content = content;
        }

        public static async Task<PagedList<GetPostCommentDto>> From<T>(PagedList<PostComment> comments)where T : PagedList<PostComment>
        {
            List<GetPostCommentDto> mappedComments =[];
            foreach (var comment in comments)
            {
                string username = $"{comment.User.FirstName} {comment.User.LastName}";

                mappedComments.Add(new GetPostCommentDto(comment.Id, comment.UserId, username, comment.CreatedAt, comment.EditedAt, comment.Content));
            }


            return new PagedList<GetPostCommentDto>(mappedComments,comments.TotalCount, comments.CurrentPage,comments.PageSize);
        }
    }
}
