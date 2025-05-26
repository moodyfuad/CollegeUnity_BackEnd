using CollegeUnity.Core.Dtos.FeedBackDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CollegeUnity.Core.MappingExtensions.FeedBackExtensions.Create
{
    public static class CreateFeedBackExtention
    {
        public static Feedback ToFeedBack(this Feedback feedback, UFeedBackResponseDto dto)
        {
            return new()
            {
                Id = feedback.Id,
                FromUser = feedback.FromUser,
                Location = feedback.Location,
                Title = feedback.Title,
                Description = feedback.Description,
                Response = dto.Response,
                FeedBackStatus = dto.Status,
            };
        }

        public static Feedback ToFeedBack(this CFeedBackResponseDto dto, int userId)
        {
            return new()
            {
                UserId = userId,
                Location = dto.Location,
                Title = dto.Title,
                Description = dto.Description,
                TypeOfFeedBack = dto.TypeOfFeedBack,
            };
        }
    }
}
