using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.PostFilesDtos
{
    /// <summary>
    /// this model for create the post files
    /// </summary>
    public class CPostFileDto
    {
        [Url]
        [Required]
        public required string Path { get; set; }
        [Required]
        public required string FileExtension { get; set; }

        public int PostId { get; set; }
    }
}
