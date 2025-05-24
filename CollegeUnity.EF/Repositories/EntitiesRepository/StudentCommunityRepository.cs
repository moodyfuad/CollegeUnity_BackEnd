using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
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

        public async Task<List<int>> GetCommunitiesByStudentIdAsync(int studentId, bool isDeleted)
        {
            var communities = await _context.StudentCommunities
                .Where(sc => sc.StudentId == studentId && sc.IsDeleted == isDeleted)
                .Select(sc => sc.CommunityId)
                .Distinct()
                .ToListAsync();

            return communities;
        }

        public async Task<List<int>> GetStudentIdsInCommunity(int communityId)
        {
            return await _context.StudentCommunities
                .Where(sc => sc.CommunityId == communityId)
                .Select(sc => sc.StudentId)
                .ToListAsync();
        }

        public async Task<CommunityMemberRoles> GetStudentRoleInCommunity(int studentId, int communityId)
        {
            return await _context.StudentCommunities
                .Where(s => s.StudentId == studentId && s.CommunityId == communityId)
                .Select(r => r.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<int>? GetStudentIdInCommunity(int studentId, int communityId)
        {
            return await _context.StudentCommunities
                .Where(s => s.StudentId == studentId && s.CommunityId == communityId)
                .Select(r => r.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetUnreadMessagesFromLastSeen(int studentId, int communityId)
        {
            var lastSeen = await _context.StudentCommunities
                .Where(s => s.StudentId == studentId && s.CommunityId == communityId)
                .Select(s => s.LastSeen)
                .FirstOrDefaultAsync();

            return await _context.CommunityMessages
                .Where(m => m.CommunityId == communityId && m.CreatedAt > lastSeen)
                .CountAsync();

        }

        public async Task SetMyLastSeen(int studentId, int communityId)
        {
            var isExist = await _context.StudentCommunities.FirstOrDefaultAsync(s => s.StudentId == studentId && s.CommunityId == communityId);
            if (isExist != null)
            {
                isExist.LastSeen = DateTime.Now;
                _context.StudentCommunities.Update(isExist);
            }
        }
    }
}
