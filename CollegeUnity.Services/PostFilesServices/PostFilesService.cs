using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.PostFilesDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostFilesExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.PostFilesServices
{
    public class PostFilesService : IPostFilesServices
    {
        private readonly IRepositoryManager _repositoryManager;
        public PostFilesService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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
                postFiles.Add(postFile);
                await FileExtentionhelper.SaveFileAsync(postFile.Path, file);
            }
            return postFiles;
        }
    }
}
