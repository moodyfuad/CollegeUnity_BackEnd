using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.InterestedSubjectExtensions
{
    public static class InterestedSubjectMapping
    {
        public static IEnumerable<GInterestedSubjectDto> toInterestedSubject(this IEnumerable<Subject> subjects)
        {
            return subjects.Select(subject => new GInterestedSubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                AcceptanceType = subject.AcceptanceType,
                Level = subject.Level,
                Major = subject.Major,
                TeacherName = subject.Teacher.FirstName + " " + subject.Teacher.MiddleName + " " + subject.Teacher.LastName
            });
        }
    }
}
