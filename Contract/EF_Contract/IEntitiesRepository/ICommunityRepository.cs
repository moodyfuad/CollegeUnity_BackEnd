using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface ICommunityRepository : IBaseRepository<Community>
    {
        Task<bool> IsExistByNameAsync(string name);
        Task<CommunityType> TypeOfCommunity(int communityId);
    }
}
