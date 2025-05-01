using CollegeUnity.Contract;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services
{
    // todo: delete testing implementation
    public class Testing : ITesting
    {
        private readonly IRepositoryManager _repos;

        public Testing(IRepositoryManager repos)
        {
            _repos = repos;
        }

        public async Task<PagedList<Student>> GetAllStudents(TestingQS queryString)
        {
            return await _repos.StudentRepository.GetRangeAsync(queryString);
        }

        public async Task<PagedList<Staff>> GetStaffMembers(TestingQS queryString)
        {
            return await _repos.StaffRepository.GetRangeAsync(queryString);
        }
    }
}
