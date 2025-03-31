using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures
{
    public class ActionFilterHelpers(IRepositoryManager repositories) : IActionFilterHelpers
    {
        public async Task<T?> IsExist<T>(int id)
           where T : class
        {
            T? result = await repositories.FindById<T>(id);
            return result;
        }
    }
}
