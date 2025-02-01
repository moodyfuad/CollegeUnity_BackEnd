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

        private IStudentRepository _studentRepository;
        private IStaffRepository _staffRepository;
        public RepositoryManager(CollegeUnityDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
