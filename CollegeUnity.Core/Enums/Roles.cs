using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Enums
{
    public enum Roles
    {
        Admin,
        Dean,
        Teacher,
        StudentAffairsViceDeanShip,
        RegistrationAdmissionEmployee,
        HeadOfScientificDepartment,
        Student
            
    }
    public static class RoleExtensions
    {
        public static string AsString(this Roles roles)
        {
            switch (roles)
            {
                case Roles.Admin:
                    return "Admin";
                case Roles.Dean:
                    return "Dean";
                case Roles.Teacher:
                    return "Teacher";
                case Roles.StudentAffairsViceDeanShip:
                    return "StudentAffairsViceDeanShip";
                case Roles.RegistrationAdmissionEmployee:
                    return "Registration Admission Employee";
                case Roles.HeadOfScientificDepartment:
                    return "Head Of Scientific Department";
                case Roles.Student:
                    return "Student";
                default:
                    return "No Role";
            }
        }
    }
}
