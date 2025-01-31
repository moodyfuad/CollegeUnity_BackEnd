//using AutoMapper;
using CollegeUnity.Contract;
using CollegeUnity.Services.AuthenticationServices;
using CollegeUnity.Services.ServiceAbstraction;
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
        //private readonly IMapper _mapper;

        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
            //_mapper = mapper;
        }

        public IAuthenticationService AuthenticationService => new AuthenticationService(_repositoryManager, _configuration);
    }
}
