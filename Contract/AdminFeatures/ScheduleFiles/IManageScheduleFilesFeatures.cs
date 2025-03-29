using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.ScheduleFilesDtos.Create;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.ScheduleFiles
{
    public interface IManageScheduleFilesFeatures
    {
        //Add
        Task<ResultDto> AddSchedule(CScheduleFilesDto dto);
        //Update
        Task<ResultDto> UpdateSchedule(int scheduleFileId, IFormFile scheduleFilePicture);
        //Delete
        Task<ResultDto> DeleteSchedule(int scheduleFileId);
    }
}
