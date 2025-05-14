using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Helpers;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Helpers
{
    public interface ISearchUsersFeature
    {
        Task<ApiResponse<PagedList<GetStudentSearchUsersResultDto>>> SearchStaff(StudentSearchUsersQS queryString);
    }
}
