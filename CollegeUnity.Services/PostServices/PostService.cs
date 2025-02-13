using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Create;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using Microsoft.AspNetCore.Http;
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
        private readonly ISubjectServices _subjectsServices;
        public PostService(IRepositoryManager repositoryManager, IPostFilesServices postFilesServices, ISubjectServices subjectsServices)
        {
            _repositoryManager = repositoryManager;
            _postFilesServices = postFilesServices;
            _subjectsServices = subjectsServices;
        }

        #region Create Posts
        public async Task CreatePublicPostAsync(CPublicPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await _createPostFiles(dto.PictureFiles, post.Id);
            }
        }

        public async Task CreateBatchPostAsync(CBatchPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await _createPostFiles(dto.PictureFiles, post.Id);
            }
        }

        public async Task CreateSubjectPostAsync(CSubjectPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await _createPostFiles(dto.PictureFiles, post.Id);
            }
        }
        #endregion

        #region Get Posts
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

        public async Task<IEnumerable<GBatchPostDto>> GetPublicAndBatchPostAsync(PublicAndBatchPostParameters batchPostParameters)
        {
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => p.IsPublic == true && p.ForMajor == batchPostParameters.ForMajor &&
                p.ForLevel == batchPostParameters.ForLevel &&
                p.ForAcceptanceType == batchPostParameters.ForAcceptanceType,
                batchPostParameters,
                [
                    i => i.PostFiles,
                    i => i.Staff
                ]);
            return posts.ToGPostMappers<GBatchPostDto>();
        }

        public async Task<IEnumerable<GStudentBatchPost>> GetSubjectPostsForStudent(StudentSubjectPostParameters parameters)
        {
            List<int> subjects = await _subjectsServices.GetStudentSubject(parameters.ForLevel, parameters.ForMajor, parameters.ForAcceptanceType);
            IEnumerable<Post> posts = await _repositoryManager.PostRepository.GetRangeByConditionsAsync(
                p => subjects.Contains((int)p.SubjectId),
                parameters,
                [
                    i => i.PostFiles,
                    i => i.Staff,
                    i => i.Subject
                ]);
            return posts.ToGPostMappers<GStudentBatchPost>();
        }
        #endregion        

        public Task<bool> DeleteAsync()
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        #region private methods
        private async Task _createPostFiles(List<IFormFile> pictureFiles, int postId)
        {
            await _postFilesServices.CreatePostFiles(pictureFiles, postId);
        }
        #endregion
    }
}
