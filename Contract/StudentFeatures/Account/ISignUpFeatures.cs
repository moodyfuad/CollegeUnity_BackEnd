using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Account
{
    public interface ISignUpFeatures
    {
        Task<ApiResponse<string?>> SignUpStudent(StudentSignUpDto studentDto);
    }
}
