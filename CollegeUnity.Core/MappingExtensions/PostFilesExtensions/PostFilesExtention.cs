using CollegeUnity.Core.Dtos.PostFilesDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.PostFilesExtensions
{
    public static partial class PostFilesExtention
    {
        public static PostFile ToPostFile<T>(this IFormFile file, int postId) where T : PostFile
        {
            return new PostFile
            {
                Path = FileExtentionhelper.GetPostPicturePath(postId, file),
                FileExtension = Path.GetExtension(file.FileName),
                PostId = postId,
            };
        }
    }
}
