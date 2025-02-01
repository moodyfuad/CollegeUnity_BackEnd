using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminServices
{
    public partial class AdminService : IAdminServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public AdminService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

    }
}
