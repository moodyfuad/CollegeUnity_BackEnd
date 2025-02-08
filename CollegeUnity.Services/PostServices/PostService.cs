using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Create;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
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

        public async Task<IEnumerable<GPublicPostDto>> GetPublicPostAsync(PublicPostParameters postParameters)
        {
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.IsPublic == true,
                postParameters,
                [
                    i => i.PostFiles,
                    i => i.Staff
                ]
            );
            return posts.ToGPostMappers<GPublicPostDto>();
        }

        public async Task CreateBatchPostAsync(CBatchPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await _postFilesServices.CreatePostFiles(dto.PictureFiles, post.Id);
            }
        }

        public async Task<IEnumerable<GBatchPostDto>> GetBatchPostAsync(BatchPostParameters batchPostParameters)
        {
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.IsPublic == false && p.ForMajor == batchPostParameters.ForMajor &&
                p.ForLevel == batchPostParameters.ForLevel &&
                p.ForAcceptanceType == batchPostParameters.ForAcceptanceType,
                batchPostParameters,
                [
                    i => i.PostFiles,
                    i => i.Staff
                ]
            );
            return posts.ToGPostMappers<GBatchPostDto>();
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
