using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Contract.StudentFeatures.Post;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.PostDtos.Update;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollegeUnity.API.Controllers.Post
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IGetPublicPostFeatures _getPublicPostFeatures;
        private readonly IGetBatchPostFeatures _getBatchPostFeatures;
        private readonly IGetSubjectPostFeatures _getSubjectPostFeatures;

        //private readonly IManagePublicPostsFeatures _managePublicPostsFeatures;
        //private readonly IManageBatchPostsFeatures _manageBatchPostsFeatures;
        //private readonly IManageSubjectPostsFeatures _manageSubjectPostsFeatures;

        private readonly IManageSubjectFeatures _manageSubjectFeatures;
        private readonly ICreatePostFeatures _createPostFeatures;

        private readonly IBasePost _basePost;


        public PostController(
            IGetPublicPostFeatures getPublicPostFeatures,
            IGetBatchPostFeatures getBatchPostFeatures,
            IGetSubjectPostFeatures getSubjectPostFeatures,
            IManageSubjectFeatures manageSubjectFeatures,
            ICreatePostFeatures createPostFeatures)
        {

            _getPublicPostFeatures = getPublicPostFeatures;
            _getBatchPostFeatures = getBatchPostFeatures;
            _getSubjectPostFeatures = getSubjectPostFeatures;

            _manageSubjectFeatures = manageSubjectFeatures;
            _createPostFeatures = createPostFeatures;
        }

        #region Before Refactoring the create features
        //[HttpPost("Public/Create")]
        //public async Task<IActionResult> CreatePublicPost([FromForm] CPublicPostDto dto)
        //{
        //    bool isExist = await _staffServices.IsExistAsync(dto.StaffId);
        //    if (isExist)
        //    {
        //        await _postServices.CreatePublicPostAsync(dto);
        //        return new JsonResult(ApiResponse<bool?>.Created(null));
        //    }

        //    return new JsonResult(ApiResponse<bool?>.BadRequest("Something wrong, try again."));
        //}

        //[HttpPost("Batch/Create")]
        //public async Task<IActionResult> CreateBatchPost([FromForm] CBatchPostDto dto)
        //{
        //    bool isExist = await _staffServices.IsExistAsync(dto.StaffId);
        //    if (isExist)
        //    {
        //        await _postServices.CreateBatchPostAsync(dto);
        //        return new JsonResult(ApiResponse<bool?>.Created(null));
        //    }

        //    return new JsonResult(ApiResponse<bool?>.BadRequest("Something wrong, try again."));
        //}

        //[HttpPost("Subject/Create")]
        //public async Task<IActionResult> CreateSubjectPost([FromForm] CSubjectPostDto dto)
        //{
        //    bool isSubjectStudiedByTeacher = await _subjectServices.SubjectStudyCheck(dto.SubjectId, dto.StaffId);
        //    if (isSubjectStudiedByTeacher)
        //    {
        //        await _postServices.CreateSubjectPostAsync(dto);
        //        return new JsonResult(ApiResponse<bool?>.Created(null));
        //    }

        //    return new JsonResult(ApiResponse<bool?>.BadRequest("Something wrong, try again."));
        //}
        #endregion

        #region After Refactoring the create features
        [HttpPost("Public/Create")]
        public async Task<IActionResult> CreatePublicPost([FromForm] CPublicPostDto dto)
        {
            var staffId = User.GetUserId();
            await _createPostFeatures.CreatePostAsync(dto, staffId);
            return new JsonResult(ApiResponse<bool?>.Created(null));
        }

        [HttpPost("Batch/Create")]
        public async Task<IActionResult> CreateBatchPost([FromForm] CBatchPostDto dto)
        {
            var staffId = User.GetUserId();
            var result = await _createPostFeatures.CreatePostAsync(dto, staffId);
            if (result.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(result.message));
        }

        [HttpPost("Subject/Create")]
        public async Task<IActionResult> CreateSubjectPost([FromForm] CSubjectPostDto dto)
        {
            var staffId = User.GetUserId();
            var result = await _createPostFeatures.CreatePostAsync(dto, staffId);
            if (result.success)
            {
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest(result.message));
        }
        #endregion

        #region Before refactoring Getting Features
        //[HttpGet("Public")]
        //public async Task<IActionResult> GetPublicPost([FromQuery] PublicPostParameters postParameters)
        //{
        //    var posts = await _postServices.GetPublicPostAsync(postParameters);
        //    if (posts.Count() > 0)
        //    {
        //        return new JsonResult(ApiResponse<IEnumerable<GPublicPostDto>>.Success(data: posts));
        //    }

        //    return new JsonResult(ApiResponse<IEnumerable<GPublicPostDto>?>.NotFound("No Posts yet."));
        //}

        //[HttpGet("PublicAndBatch")]
        //public async Task<IActionResult> GetBatchPost([FromQuery] PublicAndBatchPostParameters batchPostParameters)
        //{
        //    var posts = await _postServices.GetPublicAndBatchPostAsync(batchPostParameters);
        //    if (posts.Count() > 0)
        //    {
        //        return new JsonResult(ApiResponse<IEnumerable<GBatchPostDto>>.Success(data: posts));
        //    }

        //    return new JsonResult(ApiResponse<IEnumerable<GBatchPostDto>?>.NotFound("No Posts yet."));
        //}

        //[HttpGet("Batch")]
        //public async Task<IActionResult> GetStudentBatchPost([FromQuery] SubjectPostParameters postParameters)
        //{
        //    var posts = await _postServices.GetSubjectPostsForStudent(postParameters);
        //    if (posts.Count() > 0)
        //    {
        //        return new JsonResult(ApiResponse<IEnumerable<GStudentBatchPost>>.Success(data: posts));
        //    }

        //    return new JsonResult(ApiResponse<IEnumerable<GStudentBatchPost>?>.NotFound("No Posts yet."));
        //}
        #endregion

        #region After Refactoring the code Getting Features
        [HttpGet("Public")]
        public async Task<IActionResult> GetPublicPost([FromQuery] PublicPostParameters postParameters)
        {
            int userId = User.GetUserId();
            var posts = await _getPublicPostFeatures.GetPublicPostAsync(userId, postParameters);
            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<PagedList<GPublicPostDto>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<PagedList<GPublicPostDto>?>.NotFound("No Posts yet."));
        }

        [HttpGet("Batch")]
        public async Task<IActionResult> GetBatchPost([FromQuery] SubjectPostParameters postParameters)
        {
            var studentId = User.GetUserId();
            var posts = await _getBatchPostFeatures.GetBatchPost(studentId, postParameters);

            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<PagedList<GStudentBatchPost>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<PagedList<GStudentBatchPost>?>.NotFound("No Posts yet."));
        }


        [HttpGet("Subject")]
        public async Task<IActionResult> GetSubjectPost([FromQuery] GetSubjectPostParameters Parameters)
        {
            var studentId = User.GetUserId();
            var posts = await _getSubjectPostFeatures.GetSubjectPosts(studentId, Parameters);
            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<PagedList<GSubjectPostDto>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<PagedList<GStudentBatchPost>?>.NotFound("No Posts yet."));
        }
        #endregion

        [HttpPut("Post/Update/{postId}")]
        public async Task<IActionResult> UpdateMyPost(int postId, [FromForm]UUpdatePostDto dto)
        {
            var staffId = User.GetUserId();

            int newPicCount = dto.NewPictures?.Count ?? 0;
            int oldPicCount = dto.ExistingPictureIds?.Count ?? 0;

            if (oldPicCount < 4 && newPicCount < 4 && (newPicCount - oldPicCount) == 0)
            {
                if (await _basePost.UpdatePostAsync(postId, staffId, dto))
                {
                    return new JsonResult(ApiResponse<bool?>.Success(null));
                }
                return new JsonResult(ApiResponse<bool?>.Unauthorized("Post is not yours."));
            }

            return new JsonResult(ApiResponse<bool?>.NotFound("The uploaded pictures not true, try again."));
        }

        [HttpDelete("Post/Delete/{postId}")]
        public async Task<IActionResult> DeleteMyPost(int postId)
        {
            var staffId = User.GetUserId();
            
            if (await _basePost.DeleteAsync(staffId, postId))
            {
                return new JsonResult(ApiResponse<bool?>.Success(null));
            }
            return new JsonResult(ApiResponse<bool?>.Unauthorized("Post is not yours."));
        }
    }
}
