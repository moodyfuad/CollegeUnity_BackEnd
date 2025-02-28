using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Core.Dtos.PostDtos.Create;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Post
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        private readonly IGetPublicPostFeatures _getPublicPostFeatures;
        private readonly IGetBatchPostFeatures _getBatchPostFeatures;
        private readonly IGetSubjectPostFeatures _getSubjectPostFeatures;

        private readonly IManagePublicPostsFeatures _managePublicPostsFeatures;
        private readonly IManageBatchPostsFeatures _manageBatchPostsFeatures;
        private readonly IManageSubjectPostsFeatures _manageSubjectPostsFeatures;

        private readonly IManageSubjectFeatures _manageSubjectFeatures;


        public PostController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

            _getPublicPostFeatures = serviceManager.GetPublicPostFeatures;
            _getBatchPostFeatures = serviceManager.GetBatchPostFeatures;
            _getSubjectPostFeatures = serviceManager.GetSubjectPostFeatures;

            _managePublicPostsFeatures = serviceManager.managePublicPostsFeatures;
            _manageBatchPostsFeatures = serviceManager.manageBatchPostsFeatures;
            _manageSubjectPostsFeatures = serviceManager.manageSubjectPostsFeatures;

            _manageSubjectFeatures = serviceManager.manageSubjectFeatures;
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
            var isExist = await _serviceManager.IsExist<CollegeUnity.Core.Entities.Staff>(dto.StaffId);
            if (isExist != null)
            {
                await _managePublicPostsFeatures.CreatePublicPostAsync(dto);
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.NotFound("Something wrong, try again."));
        }

        [HttpPost("Batch/Create")]
        public async Task<IActionResult> CreateBatchPost([FromForm] CBatchPostDto dto)
        {
            var isExist = await _serviceManager.IsExist<CollegeUnity.Core.Entities.Staff>(dto.StaffId);
            if (isExist != null)
            {
                await _manageBatchPostsFeatures.CreateBatchPostAsync(dto);
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest("Something wrong, try again."));
        }

        [HttpPost("Subject/Create")]
        public async Task<IActionResult> CreateSubjectPost([FromForm] CSubjectPostDto dto)
        {
            bool isSubjectStudiedByTeacher = await _manageSubjectFeatures.SubjectStudyCheck(dto.SubjectId, dto.StaffId);
            if (isSubjectStudiedByTeacher)
            {
                await _manageSubjectPostsFeatures.CreateSubjectPostAsync(dto);
                return new JsonResult(ApiResponse<bool?>.Created(null));
            }

            return new JsonResult(ApiResponse<bool?>.BadRequest("Something wrong, try again."));
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
            var posts = await _getPublicPostFeatures.GetPublicPostAsync(postParameters);
            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<IEnumerable<GPublicPostDto>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<IEnumerable<GPublicPostDto>?>.NotFound("No Posts yet."));
        }

        [HttpGet("PublicAndBatch")]
        public async Task<IActionResult> GetBatchPost([FromQuery] PublicAndBatchPostParameters batchPostParameters)
        {
            var posts = await _getBatchPostFeatures.GetPublicAndBatchPostAsync(batchPostParameters);
            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<IEnumerable<GBatchPostDto>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<IEnumerable<GBatchPostDto>?>.NotFound("No Posts yet."));
        }

        [HttpGet("Batch")]
        public async Task<IActionResult> GetStudentBatchPost([FromQuery] SubjectPostParameters postParameters)
        {
            var posts = await _getSubjectPostFeatures.GetSubjectPosts(postParameters);
            if (posts.Count() > 0)
            {
                return new JsonResult(ApiResponse<IEnumerable<GStudentBatchPost>>.Success(data: posts));
            }

            return new JsonResult(ApiResponse<IEnumerable<GStudentBatchPost>?>.NotFound("No Posts yet."));
        }
        #endregion
    }
}
