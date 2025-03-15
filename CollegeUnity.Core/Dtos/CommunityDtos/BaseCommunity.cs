using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommunityDtos
{
    public class BaseCommunity
    {
        [Required(ErrorMessage = "Name is Required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "CommunityType is Required")]
        public required CommunityType CommunityType { get; set; }
        [Required(ErrorMessage = "CommunityState is Required")]
        public required CommunityState CommunityState { get; set; }
    }
}
