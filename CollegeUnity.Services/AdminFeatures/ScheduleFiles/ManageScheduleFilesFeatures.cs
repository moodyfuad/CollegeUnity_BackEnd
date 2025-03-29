using CollegeUnity.Contract.AdminFeatures.ScheduleFiles;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Create;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Update;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.ScheduleFilesExtensions.Create;
using Microsoft.AspNetCore.Http;
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

        private async Task<string> MappingFormToscheduleFilePicture(IFormFile scheduleFilePicture)
        {
            var path = FileExtentionhelper.GetscheduleFilePicturePath(scheduleFilePicture);
            await FileExtentionhelper.SaveFileAsync(path, scheduleFilePicture);
            return FileExtentionhelper.ConvertBaseDirctoryToBaseUrl(path);
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

            string path = await MappingFormToscheduleFilePicture(dto.SchedulePicture);
            var scheduleFile = dto.ToScheduleFile(path);
            await _repositoryManager.ScheduleFilesRepository.CreateAsync(scheduleFile);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> UpdateSchedule(int scheduleFileId, IFormFile scheduleFilePicture)
        {
            var scheduleFile = await _repositoryManager.ScheduleFilesRepository.GetByConditionsAsync(s => s.Id == scheduleFileId);

            if (scheduleFile == null)
            {
                return new(false, "Schedule not found.");
            }

            string path = await MappingFormToscheduleFilePicture(scheduleFilePicture);
            scheduleFile.Path = path;
            await _repositoryManager.ScheduleFilesRepository.Update(scheduleFile);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> DeleteSchedule(int scheduleFileId)
        {
            var scheduleFile = await _repositoryManager.ScheduleFilesRepository.GetByConditionsAsync(s => s.Id == scheduleFileId);

            if (scheduleFile == null)
            {
                return new(false, "Schedule not found.");
            }

            await _repositoryManager.ScheduleFilesRepository.Delete(scheduleFile);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }
    }
}
