using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.ScheduleFiles;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.ScheduleFilesExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.ScheduleFiles
{
    public class GetScheduleFilesFeatures : IGetScheduleFilesFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public GetScheduleFilesFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GScheduleFileDto>> GetSchedule(GetScheduleFileParameters parameters)
        {
            Expression<Func<ScheduleFile, bool>> condition =
                s => s.Major == parameters.Major &&
                s.AcceptanceType == parameters.AcceptanceType &&
                (parameters.ScheduleTypes == null || s.ScheduleType == parameters.ScheduleTypes);

            var scheduleFile = await _repositoryManager.ScheduleFilesRepository.GetRangeByConditionsAsync(condition, parameters);

            return scheduleFile.ToGetScheduleFiles();
        }
    }
}
