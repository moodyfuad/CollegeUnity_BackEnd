using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Authentication
{
    public interface ILoginFeatures
    {
        // Validate Credentials
            // params (email, password) => (isSuccess, Token, message)
            // params (studentRegID, password) => (isSuccess, Token, message)
    }
}
