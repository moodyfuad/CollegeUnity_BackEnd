using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Services.AdminServices;
using CollegeUnity.Services.AuthenticationServices;
using CollegeUnity.Services.StudentServices;
using CollegeUnity.Services.SubjectServices;
using EmailService;
using EmailService.EmailService;
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
        private readonly IEmailServices _emailServices;

        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration, IEmailServices emailServices)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
            _emailServices= emailServices;
        }

        public IAuthenticationService AuthenticationService => new AuthenticationService(_repositoryManager, _configuration);

        public IAdminServices AdminServices => new AdminService(_repositoryManager);

        public ISubjectServices SubjectServices => new SubjectService(_repositoryManager);

        public IStudentServices StudentServices => new StudentService(_repositoryManager, _emailServices);
    }
}
