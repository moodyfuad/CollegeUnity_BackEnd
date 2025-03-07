using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Helpers
{
    public static class ConvertToArabicHelper
    {
        public static string EducationToArabic(this EducationDegree degree)
        {
            switch (degree)
            {
                case EducationDegree.None:
                    return "بدون مؤهل";
                case EducationDegree.Bacillurias:
                    return "بكالوريوس";
                case EducationDegree.Master:
                    return "ماجستير";
                case EducationDegree.Doctor:
                    return "دكتوراه";
                case EducationDegree.Professor:
                    return "أستاذ";
                default:
                    throw new ArgumentOutOfRangeException(nameof(degree), degree, "Invalid education degree");
            }
        }

        public static string GenderToArabic(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "ذكر";
                case Gender.Female:
                    return "أنثى";
                default:
                    return "غير محدد";
            }
        }

        public static string RoleToArabic(this Roles role)
        {
            switch (role)
            {
                case Roles.Admin:
                    return "مدير النظام";
                case Roles.Dean:
                    return "عميد";
                case Roles.Teacher:
                    return "معلم";
                case Roles.StudentAffairsViceDeanShip:
                    return "وكيل شؤون الطلاب";
                case Roles.RegistrationAdmissionEmployee:
                    return "موظف القبول والتسجيل";
                case Roles.HeadOfITDepartment:
                    return "رئيس قسم تكنولوجيا المعلومات";
                case Roles.HeadOfCSDepartment:
                    return "رئيس قسم علوم الحاسوب";
                case Roles.Student:
                    return "طالب";
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, "Invalid role");
            }
        }

        public static IEnumerable<string> RolesToArabic(this IEnumerable<Roles> roles)
        {
            return roles.Select(role => role.RoleToArabic()).ToList();
        }
    }
}
