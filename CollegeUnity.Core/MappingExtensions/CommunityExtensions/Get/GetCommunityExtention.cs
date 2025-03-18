using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get
{
    public static class GetCommunityExtention
    {
        private static GCommunitesDto GetCommunity(this Community community)
        {
            return new GCommunitesDto
            {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
                CommunityState = community.CommunityState,
                CommunityType = community.CommunityType,
            };
        }

        public static IEnumerable<GCommunitesDto> ToGetCommunites(this IEnumerable<Community> communities)
        {
            return communities.Select(c => c.GetCommunity());
        }
    }
}
