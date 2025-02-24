using CollegeUnity.Core.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Account
{
    public interface ISignUpFeatures
    {
        Task<string> SignUpStudent(StudentSignUpDto studentDto);
    }
}
