using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
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
    public static class GetStudentSubjectsExtention
    {
        private static GStudentSubjectsDto GetSubject(this Subject subject)
        {
            return new()
            {
                subjectId = subject.Id,
                subjectName = subject.Name,
            };
        }

        public static PagedList<GStudentSubjectsDto> GetStudentSubjectsNames(this PagedList<Subject> subjects)
        {
            var results = subjects.Select(s => s.GetSubject()).ToList();
            return new PagedList<GStudentSubjectsDto>
            (
                items: results,
                count: subjects.Count(),
                pageNumber: subjects.CurrentPage,
                pageSize: subjects.PageSize
            );
        }
    }
}
