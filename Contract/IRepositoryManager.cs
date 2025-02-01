using CollegeUnity.Contract.IEntitiesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract
{
    public interface IRepositoryManager
    {
        IStudentRepository StudentRepository { get; }
        IStaffRepository StaffRepository { get; }
        Task SaveChangesAsync();
    }
}
