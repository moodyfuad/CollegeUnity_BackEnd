using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffServices
{
    public class StaffService : IStaffServices
    {
        private readonly IRepositoryManager _repositoryManager;
        public StaffService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<bool> IsExistAsync(int Id)
        {
            return await _repositoryManager.StaffRepository.IsExistById(Id);
        }
    }
}
