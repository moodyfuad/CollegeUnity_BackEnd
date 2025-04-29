using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostFilesExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.PostFilesFeatures
{
    public class FilesFeatures : IFilesFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        public FilesFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<string> MappingFormToProfilePicture(IFormFile profilePicture, int postId)
        {
            var path = FileExtentionhelper.GetPostPicturePath(postId, profilePicture);
            await FileExtentionhelper.SaveFileAsync(path, profilePicture);
            return FileExtentionhelper.ConvertBaseDirctoryToBaseUrl(path);
        }

        //Check on it
        public async Task CreatePostFiles(IEnumerable<IFormFile> files, int postId)
        {
            List<PostFile> posts = await MappingFormToFile(files, postId);
            await _repositoryManager.PostFilesRepository.AddRangeAsync(posts);
            await _repositoryManager.SaveChangesAsync();
        }

        //Check on it
        private async Task<List<PostFile>> MappingFormToFile(IEnumerable<IFormFile> files, int postId)
        {
            List<PostFile> postFiles = new List<PostFile>();
            foreach (var file in files)
            {
                PostFile postFile = file.ToPostFile<PostFile>(postId);
                await FileExtentionhelper.SaveFileAsync(postFile.Path, file);
                postFile.Path = FileExtentionhelper.ConvertBaseDirctoryToBaseUrl(postFile.Path);
                postFiles.Add(postFile);
            }
            return postFiles;
        }
    }
}
