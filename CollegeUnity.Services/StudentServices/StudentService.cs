using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentServices
{
    public partial class StudentService : IStudentServices
    {
        private readonly IRepositoryManager _repositoryManager;
        public StudentService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<Student>> GetStudentsAsync(SearchParameters parameters)
        {
            var students = await _GetStudentsAsync(parameters);

            return students;
        }
    }
}
