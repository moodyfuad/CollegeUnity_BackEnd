using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Subject
{
    public interface IStudentSubjectFeatures
    {
        Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType);
    }
}
