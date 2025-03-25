using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.VoteDtos;

namespace CollegeUnity.Core.Dtos.PostDtos.Get
{
    public abstract class GPostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public StaffInfo Staff { get; set; }
        public IEnumerable<string>? PostFiles { get; set; }
        public IEnumerable<GVoteDto>? Votes { get; set; }

        public class StaffInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public EducationDegree EducationDegree { get; set; }

        }
    }
}
