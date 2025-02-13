using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.CommentExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.CommentServices
{
    public partial class CommentService
    {
        private async Task<AddCommentResultDto> _AddComment(AddCommentDto dto)
        {
            var post = await _repositoryManager.PostRepository.GetByIdAsync(dto.PostId);
            var user = await _repositoryManager.UserRepository.GetByIdAsync(dto.UserId);
            if (post == null)
            {
                string errorMessage = "NO Post Found";
                return AddCommentResultDto.Failed(errorMessage);
            }

            if (user == null)
            {
                string errorMessage = "NO User Found";
                return AddCommentResultDto.Failed(errorMessage);
            }
            if (string.IsNullOrEmpty(dto.Comment) || string.IsNullOrWhiteSpace(dto.Comment))
            {
                string errorMessage = "Empty Comment is not allowed";
                return AddCommentResultDto.Failed(errorMessage);
            }
            else
            {
                var comment = dto.To<PostComment>(post, user);
                comment = await _repositoryManager.CommentRepository.CreateAsync(comment);
                await _repositoryManager.SaveChangesAsync();

                var result = comment == null ?
                    AddCommentResultDto.Failed("Comment Not Added") :
                    AddCommentResultDto.Success(comment.Content);

                return result;
            }
        }
    }
}
