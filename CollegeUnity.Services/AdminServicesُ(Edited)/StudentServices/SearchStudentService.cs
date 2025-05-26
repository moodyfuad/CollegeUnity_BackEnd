//using CollegeUnity.Core.Dtos.QueryStrings;
//using CollegeUnity.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollegeUnity.Services.AdminServices
//{
//    public partial class AdminService
//    {
//        private async Task<IEnumerable<Student>> _SearchStudentBy(string name, StudentParameters studentParameters)
//        {
//            return await _repositoryManager.StudentRepository.GetRangeByConditionsAsync(
//                s => s.FirstName.Contains(name), studentParameters);
//        }
//    }
//}
