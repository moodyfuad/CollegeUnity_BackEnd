using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class StudentCommunityRepository : BaseRepository<StudentCommunity>, IStudentCommunityRepository
    {
        private readonly CollegeUnityDbContext _context;

        public StudentCommunityRepository(CollegeUnityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<StudentCommunity, bool>> predicate)
        {
            return await _context.StudentCommunities.AnyAsync(predicate);
        }
    }
}
