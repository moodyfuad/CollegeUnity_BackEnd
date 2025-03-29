using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Create;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.ScheduleFilesExtensions.Create
{
    public static class CreateScheduleFilesExtension
    {
        public static ScheduleFile ToScheduleFile(this CScheduleFilesDto dto, string path)
        {
            return new()
            {
                AcceptanceType = dto.AcceptanceType,
                FileExtension = FileExtentionhelper.GetFileExtention(dto.SchedulePicture.FileName),
                Major = dto.Major,
                Path = path,
                ScheduleType = dto.ScheduleType,
                Level = dto.Level
            };
        }
    }
}
