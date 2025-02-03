using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Services.AdminServices;
using CollegeUnity.Services.AuthenticationServices;
using CollegeUnity.Services.StudentServices;
using CollegeUnity.Services.SubjectServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;

        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
        }

        public IAuthenticationService AuthenticationService => new AuthenticationService(_repositoryManager, _configuration);

        public IAdminServices AdminServices => new AdminService(_repositoryManager);

        public ISubjectServices SubjectServices => new SubjectService(_repositoryManager);

        public IStudentServices StudentServices => new StudentService(_repositoryManager);
    }
}
