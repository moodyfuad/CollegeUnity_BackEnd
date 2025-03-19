using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CommunityDtos.Get
{
    public class GStudentCommunitesDto
    {        
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
