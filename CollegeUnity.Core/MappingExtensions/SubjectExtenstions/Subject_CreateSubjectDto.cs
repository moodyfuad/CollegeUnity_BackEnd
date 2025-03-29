using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using Microsoft.VisualBasic;
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
        public static Subject MapTo(this CSubjectDto dto)
        {
            return new()
            {
                Name = dto.Name,
                Level = dto.Level,
                Major = dto.Major,
                AcceptanceType = dto.AcceptanceType,
                HeadOfScientificDepartmentId = dto?.HeadOfScientificDepartmentId ?? null,
            };
        }

        private static GSubjectDto GetSubject(this Subject subject)
        {
            return new GSubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Level = subject.Level,
                Major = subject.Major,
                AcceptanceType = subject.AcceptanceType,
                AssignedById = subject.HeadOfScientificDepartmentId ?? null,
                AssignedByName = subject.AssignedBy == null ? null : subject.AssignedBy.FirstName + " " + subject.AssignedBy.MiddleName + " " + subject.AssignedBy.LastName,
                TeacherId = subject.TeacherId ?? null,
                TeacherName = subject.Teacher == null ? null : subject.Teacher.FirstName + " " + subject.Teacher.MiddleName + " " + subject.Teacher.LastName
            };
        }

        public static PagedList<GSubjectDto> MapTo(this PagedList<Subject> subjects)
        {
            var results = subjects.Select(c => c.GetSubject()).ToList();
            var pagedList = new PagedList<GSubjectDto>
            (
                items: results,
                count: subjects.Count(),
                pageNumber: subjects.CurrentPage,
                pageSize: subjects.PageSize
            );
            return pagedList;
        }

        public static Subject MapTo(this SubjectDto dto, Subject oldSubejct)
        {
            return new()
            {
                Id = oldSubejct.Id,
                Name = dto?.Name ?? oldSubejct.Name,
                Level = dto?.Level ?? oldSubejct.Level,
                Major = dto?.Major ?? oldSubejct.Major,
                AcceptanceType = dto?.AcceptanceType ?? oldSubejct.AcceptanceType,
                HeadOfScientificDepartmentId = dto?.AssignedById ?? oldSubejct.HeadOfScientificDepartmentId,
                TeacherId = oldSubejct.TeacherId
            };
                
        }

    }
}
