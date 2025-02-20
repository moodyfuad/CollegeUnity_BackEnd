using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.VoteDtos
{
    public class GetPostVotesResultDto
    {
        public static GetPostVotesResultDto New(Post post)
        {
            if (post == null || post.Votes == null || post.Votes.Count == 0)
            {
                return new GetPostVotesResultDto(post?.Id ?? 0);
            }
            return new(post);
        }

        private GetPostVotesResultDto(Post post)
        {
            PostId = post.Id;
            TotalVotes = GetTotalVotesNumber(post);
            Votes = SelectedVote.FromPostVotes(post.Votes, TotalVotes);
        }
       
        private GetPostVotesResultDto(int postId)
        {

            PostId = postId;
            TotalVotes = 0;
            Votes = [];
        }


        public int PostId { get;}   

        public int TotalVotes { get;}   
        
        public List<SelectedVote> Votes { get; }

        private int GetTotalVotesNumber(Post post)
        {
            var temp = post.Votes?.Select(v => v.SelectedBy?.Count ?? 0);
            int result = 0;
            foreach (var vote in temp)
            {
                result += vote;
            }
            return result;
        }

        public class SelectedVote
        {
            public int Id { get; }
            public string Value { get; }

            public int Percentage { get; }

            public List<SelectedBy> selectedBy { get; }

            public SelectedVote(int id, string value, List<SelectedBy> selectedBy, int totalVotes)
            {
                Id = id;
                Value = value;
                this.selectedBy = selectedBy;
                Percentage = (int)Math.Ceiling(((double)selectedBy.Count / totalVotes) * 100);
            }


            public static SelectedVote FromPostVote(PostVote? vote, int totalVotes)
            { 
                return new(vote.Id, vote.Name, SelectedBy.FromUsers(vote.SelectedBy), totalVotes);
            }

            public static List<SelectedVote> FromPostVotes(ICollection<PostVote>? votes, int totalvotes)
            {
                List<SelectedVote> selectedVotes = [];
                if (votes == null)
                {
                    return selectedVotes;
                }
                foreach (var vote in votes)
                {
                    selectedVotes.Add(FromPostVote(vote, totalvotes));
                }
                return selectedVotes;
            }
        }

        public class SelectedBy
        {
            public SelectedBy(int userId, string name)
            {
                this.userId = userId;
                Name = name;
            }
            public int userId { get; }
            public string Name { get; }

            public static SelectedBy FromUser(User user)
            {
                string name = $"{user.FirstName} {user.LastName}";
                return new(user.Id, name);
            }

            public static List<SelectedBy> FromUsers(ICollection<User>? users)
            {
                List<SelectedBy> usersResult = [];
                if (users == null)
                {
                    return usersResult;
                }
                foreach (var user in users)
                {
                    usersResult.Add(FromUser(user));
                }
                return usersResult;
            }


        }

    }
}
