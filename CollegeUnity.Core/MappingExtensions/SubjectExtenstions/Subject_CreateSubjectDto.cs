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
    }
}
