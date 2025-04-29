using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts.PostFiles
{
    public interface IFilesFeatures
    {
        public Task CreatePostFiles(IEnumerable<IFormFile> files, int postId);
        Task<string> MappingFormToProfilePicture(IFormFile profilePicture, int postId);
    }
}
