using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CollegeUnity.Core.Dtos.PostDtos.Create
{
    /// <summary>
    /// this class for create public post
    /// </summary>
    public class CPublicPostDto : CPostDto
    {
        public override Post ToPost(int staffId)
        {
            return new Post
            {
                Content = this.Content,
                CreatedAt = DateTime.Now,
                Priority = this.Priority,
                IsPublic = true,
                StaffId = staffId,
            };
        }
    }
}
