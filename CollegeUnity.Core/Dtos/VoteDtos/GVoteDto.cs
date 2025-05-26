using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.VoteDtos
{
    public class GVoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VoteCount { get; set; }
        public double VotesPercentage { get; set; }
        public bool IsVoted { get; set; }

    }
}
