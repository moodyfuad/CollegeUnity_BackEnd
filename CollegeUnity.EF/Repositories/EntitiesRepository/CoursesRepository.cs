using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class CoursesRepository : BaseRepository<Course>, ICoursesRepository
    {
        private readonly CollegeUnityDbContext _context;

        public CoursesRepository(CollegeUnityDbContext context)
            : base(context)
        {
            _context = context;
        }

        public new async Task<Course> Delete(int id)
        {
            var entity1 = await GetByIdAsync(id);

            return _context.Courses.Remove(entity1).Entity;
        }
    }
}
