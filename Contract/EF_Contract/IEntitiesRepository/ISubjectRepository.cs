using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        Task<bool> IsExistById(int id);
        Task<List<int>> GetDistinctSubjects(Level level, Major major, AcceptanceType acceptanceType);
        Task<PagedList<Subject>?> GetIntresetedSubject(int studentId, GetInterestedSubjectParameters getInterestedSubjectParameters);
    }
}
