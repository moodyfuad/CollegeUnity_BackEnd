using CollegeUnity.Core.Dtos.VoteDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.VoteExtentions.Get
{
    public static class VoteExtention
    {
        private static GVoteDto MapToGVoteDto(this PostVote postVote, int totalVotes)
        {
            return new GVoteDto
            {
                Id = postVote.Id,
                Name = postVote.Name,
                VoteCount = postVote.SelectedBy?.Count ?? 0,
                VotesPercentage = totalVotes > 0 ? (postVote.SelectedBy?.Count ?? 0) * 100.0 / totalVotes : 0
            };
        }

        public static IEnumerable<GVoteDto> MapToGVotesDto(this IEnumerable<PostVote> postVotes)
        {
            int totalVotes = postVotes.Sum(pv => pv.SelectedBy?.Count ?? 0);

            var gVoteDtos = postVotes.Select(pv => MapToGVoteDto(pv, totalVotes)).ToList();

            return gVoteDtos;
        }
    }
}
