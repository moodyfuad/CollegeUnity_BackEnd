using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.SubjectExtenstions
{
    public static partial class SubjectExtention
    {
        public static Subject MapTo<T>(this CreateSubjectDto dto) where T : Subject
        {
            return new()
            {
                Name = dto.Name,
                Level = dto.Level,
                Major = dto.Major,
                AcceptanceType = dto.AcceptanceType,
                TeacherId = dto.TeacherId,
                HeadOfScientificDepartmentId = dto.HeadOfScientificDepartmentId,
            };
        }

        public static IEnumerable<SubjectDto> MapTo<T>(this IEnumerable<Subject> subjects) where T : SubjectDto, new()
        {
            foreach (var subject in subjects)
            {
                yield return new T
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    Level = subject.Level,
                    Major = subject.Major,
                    AcceptanceType = subject.AcceptanceType,
                    AssignedById = subject.HeadOfScientificDepartmentId == null? null : subject.HeadOfScientificDepartmentId!.Value,
                    AssignedByName = subject.AssignedBy == null? null : subject.AssignedBy.FirstName + " " + subject.AssignedBy.MiddleName + " " + subject.AssignedBy.LastName,
                    TeacherId = subject.TeacherId == null? null : subject.TeacherId!.Value,
                    TeacherName = subject.Teacher == null? null : subject.Teacher.FirstName + " " + subject.Teacher.MiddleName + " " + subject.Teacher.LastName,
                };
            }
        }

        public static Subject MapTo<T>(this SubjectDto dto) where T : Subject
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                Major = dto.Major,
                AcceptanceType = dto.AcceptanceType,
                TeacherId = dto.TeacherId,
                HeadOfScientificDepartmentId = dto.AssignedById
            };
                
        }

    }
}
