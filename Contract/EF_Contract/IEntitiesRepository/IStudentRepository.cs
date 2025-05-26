using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract.IEntitiesRepository
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> GetByEmail(string email);
        Task<string?> GetFullName(int id);
        Task ChangeStateUpgradeLevelForStudents(bool isOpen);
    }
}
