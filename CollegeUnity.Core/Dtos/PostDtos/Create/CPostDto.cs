using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.PostDtos.Create
{
    public abstract class CPostDto
    {
        [Required]
        public required string Content { get; set; }

        [Required]
        public required Priority Priority { get; set; }

        public List<IFormFile>? PictureFiles { get; set; }
        public List<string>? Votes { get; set; }
        public abstract Post ToPost(int staffId);
    }
}
