using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Communites
{
    public interface IManageCommunityFeatures
    {
        // Add Community
        Task<ResualtDto> CreateCommunityAsync(CCommunityDto dto);
        // Community Super Admin
        // Edit Info
        // Get Admins
        // Add Admin
        // Remove Admin
        // Change State
    }
}
