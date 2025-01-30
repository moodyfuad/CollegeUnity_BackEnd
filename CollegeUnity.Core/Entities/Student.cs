using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class Student : User
    {
        [Length(11,11)]
        [Required]
        public required string CardId { get; set; }
        [Required]
        [Url]
        public required string CardIdPicturePath { get; set; }
        
        [Required]
        public required Major Major { get; set; }
        [Required]
        public required Level Level { get; set; }
        [Required]
        public required AcceptanceType AcceptanceType { get; set; }
        [Required]
        public bool IsLevelEditable { get; set; } = false;

        [InverseProperty(nameof(Request.Student))]
        public virtual ICollection<Request>? Requests { get; set; }

        [InverseProperty(nameof(Subject.InterestedStudents))]
        public virtual ICollection<Subject>? InterestedSubjects { get; set; }

        [InverseProperty(nameof(Course.RegisteredStudents))]
        public virtual ICollection<Course>? RegisteredCourses { get; set; }


        public virtual ICollection<StudentCommunity>? StudentCommunity { get; set; }

    }
}
