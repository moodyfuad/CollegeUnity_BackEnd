using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Create;
using CollegeUnity.Services.StaffFeatures.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Posts
{
    public class ManageSubjectPostsFeatures : ManagePost, IManageSubjectPostsFeatures
    {
        public ManageSubjectPostsFeatures(IRepositoryManager repositoryManager, IPostFilesFeatures postFilesFeatures) : base(postFilesFeatures, repositoryManager)
        {
        }
        public async Task CreateSubjectPostAsync(CSubjectPostDto dto)
        {
            Post post = dto.ToPost<Post>();
            post = await _repositoryManager.PostRepository.CreateAsync(post);
            await _repositoryManager.SaveChangesAsync();

            if (dto.PictureFiles != null)
            {
                await createPostFiles(dto.PictureFiles, post.Id);
            }
        }
    }
}
