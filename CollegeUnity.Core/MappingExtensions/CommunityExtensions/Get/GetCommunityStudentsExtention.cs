using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get
{
    public static class GetCommunityStudentsExtention
    {
        public static GCommunityStudentsDto GetStudents(this StudentCommunity student)
        {
            return new()
            {
                Id = student.StudentId,
                Name = string.Concat(student.Student.FirstName, " ", student.Student.MiddleName, " ", student.Student.LastName),
                Role = student.Role
            };
        }
    }
}
