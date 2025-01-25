using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class PostFile
    {
        [Key]
        public int Id { get; set; }

        [Url]
        [Required]
        public required string Path { get; set; }

        [Required]
        public required string FileExtension { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public required virtual Post Post { get; set; }

    }
}
