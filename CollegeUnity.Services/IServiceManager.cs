using CollegeUnity.Services.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
    }
}
