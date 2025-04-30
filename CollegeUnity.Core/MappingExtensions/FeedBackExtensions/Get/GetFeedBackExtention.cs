using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.FeedBackDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.FeedBackExtensions.Get
{
    public static class GetFeedBackExtention
    {
        private static GFeedBackDto GetFeedBack(this Feedback feedback)
        {
            return new()
            {
                Id = feedback.Id,
                Description = feedback.Description,
                Location = feedback.Location,
                Title = feedback.Title,
                FeedBackStatus = feedback.FeedBackStatus,
                Response = feedback.Response ?? "",
                
                User = new()
                {
                    Id = feedback.UserId,
                    Name = feedback.FromUser.FirstName + " " + feedback.FromUser.LastName,
                }
            };
        }

        public static PagedList<GFeedBackDto> GetFeedBacks(this PagedList<Feedback> feedbacks)
        {
            var results = feedbacks.Select(s => s.GetFeedBack()).ToList();
            var pagedList = new PagedList<GFeedBackDto>
            (
                items: results,
                count: feedbacks.Count(),
                pageNumber: feedbacks.CurrentPage,
                pageSize: feedbacks.PageSize
            );
            return pagedList;
        }
    }
}
