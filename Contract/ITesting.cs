using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract
{
    // todo: delete testing interface
    public interface ITesting
    {
        Task<PagedList<Student>> GetAllStudents(TestingQS queryString);

        Task<PagedList<Staff>> GetStaffMembers(TestingQS queryString);
    }
}
