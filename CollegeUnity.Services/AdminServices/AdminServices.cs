using CollegeUnity.Contract;
using CollegeUnity.Services.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminServices
{
    public partial class AdminServices : IAdminServices
    {

        public AdminServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

    }
}
