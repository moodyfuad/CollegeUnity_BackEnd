using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Enums;

namespace CollegeUnity.Core.Dtos.FeedBackDtos.Get
{
    public class GFeedBackDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Location { get; set; }
        public string? Response { get; set; }
        public required enFeedBackStatus FeedBackStatus { get; set; }
        public required UserInfo User { get; set; }

        public class UserInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
