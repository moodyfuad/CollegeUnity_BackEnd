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
    public class CPublicPostDto
    {

        [Required]
        public required string Content { get; set; }

        [Required]
        public required Priority Priority { get; set; }

        [Required]
        public required int StaffId { get; set; }
        public List<IFormFile>? PictureFiles {  get; set; }
    }
}
