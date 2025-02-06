using CollegeUnity.Contract.EF_Contract.IEntitiesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.EF_Contract
{
    public interface IRepositoryManager
    {
        IStudentRepository StudentRepository { get; }
        IStaffRepository StaffRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IPostRepository PostRepository { get; }
        IPostFilesRepository PostFilesRepository { get; }
        Task SaveChangesAsync();
    }
}
