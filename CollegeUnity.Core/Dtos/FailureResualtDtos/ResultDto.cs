using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.FailureResualtDtos
{
    public class ResultDto
    {
        public bool success;
        public string? message;
        public List<string>? errors = [];

        public ResultDto(bool success, string? message)
        {
            this.success = success;
            this.message = message;
        }
        public ResultDto(bool success, string? message = null, List<string>? errors = null)
        {
            this.success = success;
            this.message = message;
            this.errors = errors;
        }
    }
}
