using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.CustomExceptions
{
    public class InternalServerException(string message, List<string>? errors = null) : Exception(message)
    {
        public readonly List<string>? Errors = errors;
    }
}
