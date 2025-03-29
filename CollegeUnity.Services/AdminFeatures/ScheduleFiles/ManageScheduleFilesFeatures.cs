using CollegeUnity.Contract.AdminFeatures.ScheduleFiles;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Create;
using CollegeUnity.Core.MappingExtensions.ScheduleFilesExtensions.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.ScheduleFiles
{
    public class ManageScheduleFilesFeatures : IManageScheduleFilesFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public ManageScheduleFilesFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ResultDto> AddSchedule(CScheduleFilesDto dto)
        {
            var check = await _repositoryManager.ScheduleFilesRepository.GetByConditionsAsync(s =>
                s.ScheduleType == dto.ScheduleType &&
                s.Major == dto.Major &&
                s.AcceptanceType == dto.AcceptanceType &&
                (dto.Level == null || s.Level == dto.Level
            ));

            if (check != null)
            {
                return new(false, "There is already Schedule added with these information.");
            }

            var scheduleFile = dto.ToScheduleFile();
            await _repositoryManager.ScheduleFilesRepository.CreateAsync(scheduleFile);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }
    }
}
