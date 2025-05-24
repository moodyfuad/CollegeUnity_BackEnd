using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.PostDtos.Create
{
    /// <summary>
    /// this class for create a subject post
    /// </summary>
    public class CSubjectPostDto : CPostDto
    {
        public int SubjectId { get; set; }

        public override Post ToPost(int staffId)
        {
            return new Post
            {
                Content = this.Content,
                CreatedAt = DateTime.Now,
                Priority = this.Priority,
                IsPublic = false,
                SubjectId = this.SubjectId,
                StaffId = staffId,
            };
        }
    }
}
