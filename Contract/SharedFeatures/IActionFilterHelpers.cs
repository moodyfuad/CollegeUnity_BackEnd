using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures
{
    public interface IActionFilterHelpers
    {
        Task<T?> IsExist<T>(int id)
            where T : class;
    }
}
