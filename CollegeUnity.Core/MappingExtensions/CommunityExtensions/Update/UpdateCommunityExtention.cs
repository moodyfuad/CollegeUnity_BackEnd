using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommunityExtensions.Update
{
    public static class UpdateCommunityExtention
    {
        public static Community ToCommunity<T>(this UCommunityInfoDto dto, int communityId) where T : Community
        {
            return new()
            {
                Id = communityId,
                Name = dto.Name,
                Description = dto.Description,
                CommunityState = dto.CommunityState,
                CommunityType = dto.CommunityType,
            };
        }
    }
}
