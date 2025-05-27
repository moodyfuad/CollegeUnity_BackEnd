//using CollegeUnity.Core.Dtos.StudentServiceDtos;
//using CollegeUnity.Core.Entities;
//using CollegeUnity.Core.Enums;
//using CollegeUnity.Core.Helpers;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollegeUnity.Services.StudentServices
//{
//    public partial class StudentService
//    {
//        private async Task<PagedList<Student>> _GetStudentsAsync(StudentSearchParameters para)
//        {
//            //var studentsAsQuery = await _repositoryManager.StudentRepository.GetAsQueryable();

//            var expressions = new List<Expression<Func<Student, bool>>>();

//            #region Strings filtering

//            if (para.CardId is not null) 
//                expressions.Add(s => s.CardId.Contains(para.CardId));

//            if (para.FirstName is not null)
//                expressions.Add(s => s.FirstName.ToLower().Contains(para.FirstName.ToLower()));

//            if (para.MiddleName is not null)
//                expressions.Add(s => s.MiddleName.ToLower().Contains(para.MiddleName));

//            if (para.LastName is not null)
//                expressions.Add(s => s.LastName.ToLower().Contains(para.LastName));

//            if (para.IsLevelEditable is not null)
//                expressions.Add(s => s.IsLevelEditable.Equals(para.IsLevelEditable));
            
//            if (para.Email is not null)
//                expressions.Add(s => s.Email.ToLower().Contains(para.Email));

            
//            if (para.Phone is not null)
//                expressions.Add(s => s.Phone.Contains(para.Phone));
//            #endregion

//            #region Enums filtering

//            if (para.Major is not null )
//                expressions.Add(s => s.Major.Equals(para.Major));

//            if (para.Level is not null)
//                expressions.Add(s => s.Level.Equals(para.Level));

//            if (para.Gender is not null)
//                expressions.Add(s => s.Gender.Equals(para.Gender));

//            if (para.AccountStatus is not null)
//                expressions.Add(s => s.AccountStatus.Equals(para.AccountStatus));

//            if (para.AcceptanceType is not null)
//                expressions.Add(s => s.AcceptanceType.Equals(para.AcceptanceType));

//            #endregion

//            return await _repositoryManager.StudentRepository.GetRangeByConditionsAsync(
//                condition: [.. expressions],para
//                );
//        }


//    }
//}
