using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IStudentRepository StudentRepository { get; }
        IStaffRepository StaffRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IPostRepository PostRepository { get; }
        IPostFilesRepository PostFilesRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICommunityRepository CommunityRepository { get; }
        IVotesRepository VotesRepository { get; }
        Task SaveChangesAsync();

        Task<T?> FindById<T>(int id)
            where T : class;
        Task<IDbContextTransaction> BeginTransactionAsync();
        void Detach<T>(T entity) where T : class;
    }
}
