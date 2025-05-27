//using CollegeUnity.Contract.EF_Contract;
//using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
//using CollegeUnity.Core.Dtos.StudentServiceDtos;
//using CollegeUnity.Core.Entities;
//using CollegeUnity.Core.Enums;
//using CollegeUnity.Core.Helpers;
//using EmailService;
//using EmailService.EmailService;
//using EmailService.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollegeUnity.Services.StudentServices
//{
//    public partial class StudentService : IStudentServices
//    {
//        private readonly IRepositoryManager _repositoryManager;
//        //private readonly IEmailServices _emailServices;

//        //public StudentService(IRepositoryManager repositoryManager, IEmailServices emailServices)
//        public StudentService(IRepositoryManager repositoryManager)
//        {
//            _repositoryManager = repositoryManager;
//            //_emailServices = emailServices;
//        }


//        public async Task<PagedList<Student>> GetStudentsAsync(StudentSearchParameters parameters)
//        {
//            var students = await _GetStudentsAsync(parameters);

//            return students;
//        }

//        public async Task<bool> CheckResetPasswordCode(string email,string code)
//        {
//            return await _CheckResetPasswordCode(email,code);
//        }
//        public async Task<bool> ResetPassword(string email,string code, string newPassword)
//        {
//            try{
//                var student = await _repositoryManager.StudentRepository.GetByConditionsAsync(s => s.Email == email && code.Equals(s.VerificationCode));

//            student.Password = newPassword;
//            student.ConfirmPassword = newPassword;
//            student = await _repositoryManager.StudentRepository.Update(student);
//            await _repositoryManager.SaveChangesAsync();

//            return student != null;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}
