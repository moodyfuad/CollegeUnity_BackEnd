using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface ISubjectServices
    {
        Task<ApiResponse<Subject>> CreateSubjectAsync(CreateSubjectDto dto);
        Task<ApiResponse<Subject>> DeleteSubject(int Id);
        Task<ApiResponse<IEnumerable<SubjectDto>>> GetAllAsync();
        Task<ApiResponse<Subject>> UpdateSubject(SubjectDto dto);
    }
}
