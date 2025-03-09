using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        private readonly CollegeUnityDbContext _context;
        public CommunityRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExistByNameAsync(string name)
        {
            return await _context.Communities.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
