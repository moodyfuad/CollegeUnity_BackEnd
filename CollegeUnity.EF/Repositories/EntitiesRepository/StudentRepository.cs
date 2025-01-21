using CollegeUnity.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Entities;
using CollegeUnity.Contract.IEntitiesRepository;

namespace CollegeUnity.EF.Repositories.EntitiesRepository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository 
    {
        public StudentRepository(CollegeUnityDbContext context) : base(context)
        {
        }
    }
}
