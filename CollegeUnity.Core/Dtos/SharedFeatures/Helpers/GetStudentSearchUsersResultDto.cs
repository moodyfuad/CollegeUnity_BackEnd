using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Helpers
{
    public class GetStudentSearchUsersResultDto
    {
        public int StaffId { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public EducationDegree Degree { get; }
        public List<string>? Subjects { get; }


        public GetStudentSearchUsersResultDto(int staffId, string firstName, string middleName, string lastName, EducationDegree degree, List<string>? subjects)
        {
            StaffId = staffId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Degree = degree;
            Subjects = subjects;
        }

        public static GetStudentSearchUsersResultDto MapFrom(Staff staff)
        {
            return new GetStudentSearchUsersResultDto(
                staffId: staff.Id,
                firstName: staff.FirstName,
                middleName: staff.MiddleName,
                lastName: staff.LastName,
                degree: staff.EducationDegree,
                subjects: staff.TeacherSubjects?.Select(sub => sub.Name).ToList() ?? null);
        }

    }
}
