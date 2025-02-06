using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IPostFilesServices
    {
        public Task CreatePostFiles(IEnumerable<IFormFile> files, int postId);
    }
}
