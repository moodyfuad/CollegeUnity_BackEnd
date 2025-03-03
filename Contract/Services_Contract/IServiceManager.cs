using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract
{
    public interface IServiceManager
    {
        //IAuthenticationService AuthenticationService { get; }
        IAdminServices AdminServices{ get; }
        ISubjectServices SubjectServices{ get; }
        IStudentServices StudentServices{ get; }
        IPostServices PostServices{ get; }
        IStaffServices StaffServices{ get; }
        IPostFilesServices PostFilesServices { get; }
        //ICommentService CommentService { get; }
        IVoteService VoteService { get; }

        Task<T?> IsExist<T>(int id)
            where T : class;
    }
}
