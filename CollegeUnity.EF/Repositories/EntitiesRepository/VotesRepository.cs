using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class VotesRepository : BaseRepository<PostVote>, IVotesRepository
    {
        private readonly CollegeUnityDbContext _context;

        public VotesRepository(CollegeUnityDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<PostVote> votes)
        {
            await _context.PostVotes.AddRangeAsync(votes);
        }

        public async Task<List<PostVote>> GetPostVotes(int postId)
        {
            return await _context.PostVotes.Where(vote => vote.PostId == postId).Include(v => v.SelectedBy).ToListAsync();
        }
    }
}
