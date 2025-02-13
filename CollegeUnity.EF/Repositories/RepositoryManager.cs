using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.EF.Repositories.EntitiesRepository;
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

        public RepositoryManager(CollegeUnityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository
        {
            get => _userRepository ??= new UserRepository(_dbContext);
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

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
