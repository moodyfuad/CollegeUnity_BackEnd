using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Get;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.ScheduleFiles
{
    public interface IGetScheduleFilesFeatures
    {
        // Get
        Task<PagedList<GScheduleFileDto>> GetSchedule(GetScheduleFileParameters parameters);
    }
}
