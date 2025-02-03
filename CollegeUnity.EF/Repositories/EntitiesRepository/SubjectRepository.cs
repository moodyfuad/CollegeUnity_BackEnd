using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly CollegeUnityDbContext _dbContext;
        public SubjectRepository(CollegeUnityDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<bool> IsExistById(int id)
        {
            return await _dbContext.Subjects.AnyAsync(s => s.Id == id);
        }
    }
}
