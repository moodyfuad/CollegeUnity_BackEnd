using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StaffFeatures;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Staffs
{
    public interface IGetStaffFeatures
    {
        Task<PagedList<GetStaffDto>> GetStaffs(int staffId, GetStaffParameters parameters);
    }
}
