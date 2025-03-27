using CollegeUnity.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Entities;
using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using CollegeUnity.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using CollegeUnity.Core.Dtos.QueryStrings;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly CollegeUnityDbContext _context;

        public StudentRepository(CollegeUnityDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Student> GetByEmail(string email)
        {
            return await GetByConditionsAsync(s => s.Email.ToLower().Equals(email.ToLower()));
        }

        public async Task<PagedList<Course>> GetCourses(int studentId, QueryStringParameters queryString)
        {
            Student student = await _context.Students.FindAsync(studentId);

            var courses = _context.Courses.Include(c => c.RegisteredStudents)
                .Where(c => c.RegisteredStudents.AsQueryable().Contains(student));

            return await PagedList<Course>.ToPagedListAsync(
                courses,
                pageNumber: queryString.PageNumber,
                pageSize: queryString.PageSize,
                desOrder: queryString.DesOrder);

        }
    }
}
