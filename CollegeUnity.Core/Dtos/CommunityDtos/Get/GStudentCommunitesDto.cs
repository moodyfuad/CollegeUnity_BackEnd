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
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UnreadCounter { get; set; }
        public string? LastMessage { get; set; }
    }
}
