using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface ICoursesRepository : IBaseRepository<Course>
    {
        new Task<Course> Delete(int id);

        Task<PagedList<Course>> GetStudentCourses(int studentId, QueryStringParameters queryString);
    }
}
