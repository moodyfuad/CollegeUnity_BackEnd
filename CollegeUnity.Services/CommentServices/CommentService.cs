using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.CommentDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;

namespace CollegeUnity.Services.CommentServices
{
    public partial class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CommentService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> IsExist(int id)
        {
            var result = await _repositoryManager.CommentRepository.GetByIdAsync(id);
            return result != null;
        }

        public async Task<AddCommentResultDto> AddComment(AddCommentDto dto)
        {
            try
            {
                return await _AddComment(dto);
            }
            catch (Exception ex)
            {
                return AddCommentResultDto.Failed(ex.Message);
            }
        }

        public async Task<DeleteCommentResultDto> DeleteComment(int commentId)
        {
            try
            {
                var result = await _DeleteComment(commentId);
                return result;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return DeleteCommentResultDto.Failed(errorMessage);
            }
        }

        public async Task<EditCommentDto> EditComment(EditCommentDto dto)
        {
            try
            {
                return await _EditComment(dto);
            }
            catch (Exception ex)
            {
                return EditCommentDto.Failed(dto.id, ex.Message);
            }
        }

        public async Task<PagedList<GetPostCommentDto>> GetPostComments(GetPostCommentsParameters param)
        {
            try
            {
                return await _GetPostComments(param);
            }
            catch (Exception ex)
            {
                throw new Exception($"internal server error {ex.Message}");
            }
        }
    }
}
