using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.ScheduleFilesExtensions.Get
{
    public static class GetScheduleFilesExtension
    {
        private static GScheduleFileDto GetScheduleFile(this ScheduleFile file)
        {
            return new()
            {
                Id = file.Id,
                AcceptanceType = file.AcceptanceType,
                Major = file.Major,
                Path = file.Path,
                ScheduleType = file.ScheduleType,
            };
        }

        public static PagedList<GScheduleFileDto> ToGetScheduleFiles(this PagedList<ScheduleFile> scheduleFiles)
        {
            var results = scheduleFiles.Select(c => c.GetScheduleFile()).ToList();
            var pagedList = new PagedList<GScheduleFileDto>
                (
                    items: results,
                    count: scheduleFiles.Count(),
                    pageNumber: scheduleFiles.CurrentPage,
                    pageSize: scheduleFiles.PageSize
                );
            return pagedList;
        }
    }
}
