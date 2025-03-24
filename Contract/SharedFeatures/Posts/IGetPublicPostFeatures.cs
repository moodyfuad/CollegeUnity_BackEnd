using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Posts
{
    public interface IGetPublicPostFeatures
    {
        // get
        public Task<IEnumerable<GPublicPostDto>> GetPublicPostAsync(PublicPostParameters postParameters);
    }
}
