using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.FailureResualtDtos
{
    public class ResualtDto
    {
        public bool success;
        public string? message;

        public ResualtDto(bool success, string? message)
        {
            this.success = success;
            this.message = message;
        }
    }
}
