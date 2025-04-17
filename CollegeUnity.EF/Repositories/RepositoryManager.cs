using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.EF.Repositories.EntitiesRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CollegeUnityDbContext _dbContext;

        private IUserRepository _userRepository;
        private IStudentRepository _studentRepository;
        private IStaffRepository _staffRepository;
        private ISubjectRepository _subjectRepository;
        private IPostRepository _postRepository;
        private IPostFilesRepository _postFilesRepository;
        private ICommentRepository _commentRepository;
        private IVotesRepository _votesRepository;
        private ICommunityRepository _communityRepository;
        private IStudentCommunityRepository _studentCommunityRepository;
        private IRequestRepository _requestRepository;
        private ICoursesRepository _coursesRepository;
        private IScheduleFilesRepository _scheduleFilesRepository;
        private IChatRepository _chatRepository;
        private IChatMessageRepository _chatMessageRepository;

        public RepositoryManager(CollegeUnityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository
        {
            get => _userRepository ??= new UserRepository(_dbContext);
            private set { }
        }

        public IStudentCommunityRepository StudentCommunityRepository
        {
            get => _studentCommunityRepository ??= new StudentCommunityRepository(_dbContext);
            private set { }
        }

        public IStudentRepository StudentRepository
        {
            get => _studentRepository ??= new StudentRepository(_dbContext);
            private set { }
        }

        public IStaffRepository StaffRepository
        {
            get => _staffRepository ??= new StaffRepository(_dbContext);
            private set { }
        }

        public ISubjectRepository SubjectRepository
        {
            get => _subjectRepository ??= new SubjectRepository(_dbContext);
            private set { }
        }

        public IPostRepository PostRepository
        {
            get => _postRepository ??= new PostRepository(_dbContext);
            private set { }
        }

        public IPostFilesRepository PostFilesRepository
        {
            get => _postFilesRepository ??= new PostFilesRepository(_dbContext);
            private set { }
        }

        public ICommentRepository CommentRepository
        {
            get => _commentRepository ??= new CommentRepository(_dbContext);
            private set { }
        }

        public IVotesRepository VotesRepository
        {
            get => _votesRepository ??= new VotesRepository(_dbContext);
            private set { }
        }

        public ICoursesRepository CoursesRepository
        {
            get => _coursesRepository ??= new CoursesRepository(_dbContext);
            private set { }
        }

        public ICommunityRepository CommunityRepository
        {
            get => _communityRepository ??= new CommunityRepository(_dbContext);
            private set { }
        }

        public IRequestRepository RequestRepository
        {
            get => _requestRepository ??= new RequestRepository(_dbContext);
            private set { }
        }

        public IScheduleFilesRepository ScheduleFilesRepository
        {
            get => _scheduleFilesRepository ??= new ScheduleFilesRepository(_dbContext);
            private set { }
        }

        public IChatMessageRepository ChatMessageRepository
        {
            get => _chatMessageRepository ??= new ChatMessageRepository(_dbContext);
            private set { }
        }

        public IChatRepository ChatRepository
        {
            get => _chatRepository ??= new ChatRepository(_dbContext);
            private set { }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> FindById<T>(int id)
            where T : class
        {
            T? result = await _dbContext.Set<T>().FindAsync(id);

            return result;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public void Detach<T>(T entity) where T : class
        {
            var entry = _dbContext.Entry(entity);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
