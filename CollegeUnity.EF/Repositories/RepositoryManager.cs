using CollegeUnity.Contract;
using CollegeUnity.Contract.IEntitiesRepository;
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

        private IStudentRepository _studentRepository;
        public RepositoryManager(CollegeUnityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_dbContext);
                }
                return _studentRepository;
            }
            private set { }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
