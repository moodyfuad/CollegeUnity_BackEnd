using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.PostDtos.Update
{
    public class UUpdatePostDto
    {
        [Required(ErrorMessage = "Content of the post")]
        public string Content { get; set; }
        public List<int>? ExistingPictureIds { get; set; }
        public List<IFormFile>? NewPictures { get; set; }

    }
}
