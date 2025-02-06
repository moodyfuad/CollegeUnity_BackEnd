using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.PostServices
{
    public class PostService : IPostServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IPostFilesServices _postFilesServices;
        public PostService(IRepositoryManager repositoryManager, IPostFilesServices postFilesServices)
        {
            _repositoryManager = repositoryManager;
            _postFilesServices = postFilesServices;
        }

        public Task<bool> CreateBatchPostAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreatePublicPostAsync(CPublicPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await _postFilesServices.CreatePostFiles(dto.PictureFiles, post.Id);
            }
        }

        public Task<bool> CreateSubjectPostAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        #region private methods for create post
        private async Task CreatePostFiles()
        {
        } 
        #endregion
    }
}
