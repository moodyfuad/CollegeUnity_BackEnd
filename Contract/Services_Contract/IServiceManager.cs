using CollegeUnity.Contract.AdminFeatures.Staffs;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Contract.SharedFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts;
using CollegeUnity.Contract.StaffFeatures.Posts.PostFiles;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Contract.StudentFeatures.Post;
using CollegeUnity.Contract.StudentFeatures.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract
{
    public interface IServiceManager
    {
        //IAuthenticationService AuthenticationService { get; }
        IAdminServices AdminServices { get; }
        IStudentServices StudentServices { get; }
        IStaffServices StaffServices { get; }
        IPostFilesServices PostFilesServices { get; }
        //ICommentService CommentService { get; }
        IVoteService VoteService { get; }

        #region Get Posts Features
        IGetPublicPostFeatures GetPublicPostFeatures { get; }
        IGetBatchPostFeatures GetBatchPostFeatures { get; }
        IGetSubjectPostFeatures GetSubjectPostFeatures { get; }
        IStudentSubjectFeatures StudentSubjectFeatures { get; }
        #endregion

        #region Manage Posts Features
        IManagePublicPostsFeatures managePublicPostsFeatures { get; }
        IManageBatchPostsFeatures manageBatchPostsFeatures { get; }
        IManageSubjectPostsFeatures manageSubjectPostsFeatures { get; }
        #endregion

        #region Base Post Features
        IBasePost basePost { get; }
        #endregion
        
        #region PostFiles Features
        IPostFilesFeatures postFilesFeatures { get; }
        #endregion

        #region Manage Subject Features
        IManageSubjectFeatures manageSubjectFeatures { get; }
        #endregion

        #region Manage staff Features
        IManageStaffFeatures manageStaffFeatures { get; }
        #endregion

        Task<T?> IsExist<T>(int id)
            where T : class;
    }
}
