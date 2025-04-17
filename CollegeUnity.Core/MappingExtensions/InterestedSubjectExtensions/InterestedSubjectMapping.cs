using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.InterestedSubjectExtensions
{
    public static class InterestedSubjectMapping
    {
        public static PagedList<GInterestedSubjectDto> toInterestedSubject(this PagedList<Subject> subjects)
        {
            var results = subjects.Select(subject => new GInterestedSubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                AcceptanceType = subject.AcceptanceType,
                Level = subject.Level,
                Major = subject.Major,
                TeacherName = subject.Teacher != null
                    ? $"{subject.Teacher.FirstName} {subject.Teacher.MiddleName} {subject.Teacher.LastName}"
                    : "No Teacher"
            }).ToList();

            return new PagedList<GInterestedSubjectDto>
            (
                items: results,
                count: subjects.Count(),
                pageNumber: subjects.CurrentPage,
                pageSize: subjects.PageSize
            );
        }
    }
}
