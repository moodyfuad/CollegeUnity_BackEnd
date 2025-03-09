using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommunityExtensions.Create
{
    public static class CreateCommunityExtention
    {
        public static Community ToCommunity<T>(this CCommunityDto dto) where T : Community
        {
            return new()
            {
                Name = dto.Name,
                Description = dto.Description,
                CommunityState = dto.CommunityState,
                CommunityType = dto.CommunityType,
            };
        }
    }
}
