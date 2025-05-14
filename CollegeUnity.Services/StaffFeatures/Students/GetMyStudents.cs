using CollegeUnity.Contract.AdminFeatures.Subjects;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Students;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.StudentExtensions.Get;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures
{
    public class GetMyStudents : IGetMyStudents
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IGetMySubjects _getMySubjects;

        public GetMyStudents(IRepositoryManager repositoryManager, IGetMySubjects getMySubjects)
        {
            _repositoryManager = repositoryManager;
            _getMySubjects = getMySubjects;
        }
        public async Task<PagedList<GStudentDto>> MyStudents(int staffId, GetMyStudentsParameters parameters)
        {
            var subjects = await _getMySubjects.MySubjects(staffId);
            PagedList<Student> matchingStudents;
            bool isNumber = double.TryParse(parameters.NameOrCardId, out _);
            var predicate = PredicateBuilder.New<Student>(false);

            foreach (var subject in subjects)
            {
                predicate = predicate.Or(s =>
                    s.Level == subject.Level &&
                    s.Major == subject.Major &&
                    s.AcceptanceType == subject.AcceptanceType
                );
            }

            if (!string.IsNullOrEmpty(parameters.NameOrCardId))
            {
                var namePredicate = PredicateBuilder.New<Student>(s =>
                    isNumber
                        ? s.CardId.StartsWith(parameters.NameOrCardId)
                        : string.Concat(s.FirstName.ToLower(), " ", s.LastName.ToLower())
                            .StartsWith(parameters.NameOrCardId.ToLower())
                );

                predicate = predicate.And(namePredicate);
            }

            matchingStudents = await _repositoryManager.StudentRepository.GetRangeByConditionsAsync(
                predicate, parameters);


            return matchingStudents.ToGetStudents();
        }
    }
}
