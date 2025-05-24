using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.PostDtos.Create
{
    /// <summary>
    /// this class for create a patch post by level, major and acceptance type
    /// </summary>
    public class CBatchPostDto : CPostDto
    {
        [Required]
        public Major? ForMajor { get; set; }
        [Required]
        public Level? ForLevel { get; set; }
        [Required]
        public AcceptanceType? ForAcceptanceType { get; set; }

        public override Post ToPost(int staffId)
        {
            return new Post
            {
                Content = this.Content,
                CreatedAt = DateTime.Now,
                Priority = this.Priority,
                ForAcceptanceType = this.ForAcceptanceType,
                ForLevel = this.ForLevel,
                ForMajor = this.ForMajor,
                IsPublic = true,
                StaffId = staffId,
            };
        }
    }
}
