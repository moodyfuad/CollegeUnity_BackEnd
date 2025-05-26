using CollegeUnity.Core.Enums;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.FeedBackDtos.Create
{
    public class CFeedBackResponseDto
    {
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }
        public string Location { get; set; }
        public enTypeOfFeedBack TypeOfFeedBack { get; set; }
    }
}
