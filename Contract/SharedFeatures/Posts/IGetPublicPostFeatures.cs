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
        public Task<PagedList<GPublicPostDto>> GetPublicPostAsync(int userId, PublicPostParameters postParameters);
    }
}
