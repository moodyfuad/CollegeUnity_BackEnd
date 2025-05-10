using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Helpers;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Helpers;
using CollegeUnity.Core.Helpers;

namespace CollegeUnity.Services.SharedFeatures.Helpers
{
    public class SearchUsersFeature(IRepositoryManager _repositories) : ISearchUsersFeature
    {
        public async Task<ApiResponse<PagedList<GetStudentSearchUsersResultDto>>> SearchStaff(StudentSearchUsersQS queryString)
        {
            var staffMembers = await _repositories.StaffRepository.GetRangeByConditionsAsync(
                condition:
                     s => (s.FirstName.Contains(queryString.FirstName) &&
                     s.LastName.Contains(queryString.LastName) &&
                     s.MiddleName.Contains(queryString.MiddleName) &&
                     (queryString.Degree.HasValue ? s.EducationDegree.Equals(queryString.Degree) : true)),
                trackChanges: false,
                queryStringParameters: queryString,
                includes: [s => s.TeacherSubjects]);

            if (staffMembers.TotalCount == 0)
            {
                throw new KeyNotFoundException("Member Not Found");
            }

            var result = staffMembers.To(GetStudentSearchUsersResultDto.MapFrom);

            return ApiResponse<PagedList<GetStudentSearchUsersResultDto>>.Success(result)!;
        }
    }
}
