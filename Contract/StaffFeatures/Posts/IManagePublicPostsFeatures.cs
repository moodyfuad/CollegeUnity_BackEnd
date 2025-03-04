using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public interface IManagePublicPostsFeatures : IBasePost
    {
        // C,U,D
        public Task CreatePublicPostAsync(CPublicPostDto dto, int staffId);
    }
}
