using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IStudentCommunityRepository : IBaseRepository<StudentCommunity>
    {
        Task<bool> AnyAsync(Expression<Func<StudentCommunity, bool>> predicate);
        Task<List<int>> GetCommunitiesByStudentIdAsync(int studentId, bool isDeleted);
        Task<int> GetUnreadMessagesFromLastSeen(int studentId, int communityId);
        Task SetMyLastSeen(int studentId, int communityId);
        Task<List<int>> GetStudentIdsInCommunity(int communityId);
        Task<CommunityMemberRoles> GetStudentRoleInCommunity(int studentId, int communityId);
        Task<int>? GetStudentIdInCommunity(int studentId, int communityId);
    }
}
