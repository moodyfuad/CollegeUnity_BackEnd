using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Community
{
    public interface IStudentCommunityFeatures
    {
        // Get
        Task<PagedList<GStudentCommunitesDto>> GetMyCommunites(int studentId, GetStudentCommunitesParameters parameters);
        // Search
        //Task<PagedList<>> SearchByName();
        // join
        //Task<PagedList<>> JoinToCommunity();
        // Leave
        //Task<PagedList<>> LeaveFromCommunity();
    }
}
