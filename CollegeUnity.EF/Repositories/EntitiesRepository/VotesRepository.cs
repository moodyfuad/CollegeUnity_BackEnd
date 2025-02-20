using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class VotesRepository : BaseRepository<PostVote>, IVotesRepository
    {
        private readonly CollegeUnityDbContext _context;

        public VotesRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
