using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.SharedFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using CollegeUnity.Services.AdminServices;
using CollegeUnity.Services.CommentServices;
using CollegeUnity.Services.PostFilesServices;
using CollegeUnity.Services.PostServices;
using CollegeUnity.Services.SharedFeatures.Posts;
using CollegeUnity.Services.StaffFeatures.Posts;
using CollegeUnity.Services.StaffFeatures.Posts.PostsVotes;
using CollegeUnity.Services.StaffFeatures.Subjects;
using CollegeUnity.Services.StaffServices;
using CollegeUnity.Services.StudentFeatures.Subjects;
using CollegeUnity.Services.StudentServices;
using CollegeUnity.Services.SubjectServices;
using CollegeUnity.Services.VoteServices;
using EmailService;
using EmailService.EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;

        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration, IEmailServices emailServices)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
            _emailServices= emailServices;
        }

        //public IAuthenticationService AuthenticationService => new AuthenticationService(_repositoryManager, _configuration,_emailServices);

        #region Get Posts Features
        public IGetPublicPostFeatures GetPublicPostFeatures => new GetPublicPostFeatures(_repositoryManager);
        public IGetBatchPostFeatures GetBatchPostFeatures => new GetBatchPostFeatures(_repositoryManager);
        public IGetSubjectPostFeatures GetSubjectPostFeatures => new GetSubjectPostFeatures(_repositoryManager, StudentSubjectFeatures);
        public IStudentSubjectFeatures StudentSubjectFeatures => new StudentSubjectFeatures(_repositoryManager);
        #endregion

        #region Manage Posts Features
        public IManagePublicPostsFeatures managePublicPostsFeatures => new ManagePublicPostsFeatures(_repositoryManager, postFilesFeatures, postVoteFeatures);
        
        public IManageBatchPostsFeatures manageBatchPostsFeatures => new ManageBatchPostsFeatures(_repositoryManager, postFilesFeatures, postVoteFeatures);

        public IManageSubjectPostsFeatures manageSubjectPostsFeatures => new ManageSubjectPostsFeatures(_repositoryManager, postFilesFeatures, postVoteFeatures);
        #endregion

        #region Base Posts Features
        public IBasePost basePost => new BasePost(_repositoryManager, postFilesFeatures, postVoteFeatures);
        #endregion

        #region PostFiles Features
        public IPostFilesFeatures postFilesFeatures => new PostFilesFeatures(_repositoryManager);
        #endregion

        #region PostVotes Features
        public IPostVoteFeatures postVoteFeatures => new PostVoteFeatures(_repositoryManager);
        #endregion

        #region Manage Subject Features
        public IManageSubjectFeatures manageSubjectFeatures => new ManageSubjectFeatures(_repositoryManager);
        #endregion

        public IAdminServices AdminServices => new AdminService(_repositoryManager);

        public ISubjectServices SubjectServices => new SubjectService(_repositoryManager);

        //public IStudentServices StudentServices => new StudentService(_repositoryManager, _emailServices);
        public IStudentServices StudentServices => new StudentService(_repositoryManager);

        public IPostServices PostServices => new PostService(_repositoryManager, PostFilesServices, SubjectServices);

        public IStaffServices StaffServices => new StaffService(_repositoryManager);

        public IPostFilesServices PostFilesServices => new PostFilesService(_repositoryManager);

        //public ICommentService CommentService => new CommentService(_repositoryManager);

        public IVoteService VoteService => new VoteService(_repositoryManager);

        public async Task<T?> IsExist<T>(int id)
            where T : class
        {
            T? result = await _repositoryManager.FindById<T>(id);
            return result;
        }
    }
}
