using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StudentExtensions.Get
{
    public static class GetStudentExtention
    {
        private static GStudentDto GetStudent(this Student student)
        {
            return new()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                BirthDate = student.BirthDate,
                AccountStatus = student.AccountStatus,
                AccountStatusReason = student.AccountStatusReason,
                CardId = student.CardId,
                CardIdPicturePath = student.CardIdPicturePath,
                Email = student.Email,
                Phone = student.Phone,
                Level = student.Level,
                AcceptanceType = student.AcceptanceType,
                Major = student.Major,
                ProfilePicturePath = student.ProfilePicturePath,
            };
        }

        public static PagedList<GStudentDto> ToGetStudents(this PagedList<Student> students)
        {
            var results = students.Select(c => c.GetStudent()).ToList();
            var pagedList = new PagedList<GStudentDto>
                (
                    items: results,
                    count: students.Count(),
                    pageNumber: students.CurrentPage,
                    pageSize: students.PageSize
                );
            return pagedList;
        }
    }
}
