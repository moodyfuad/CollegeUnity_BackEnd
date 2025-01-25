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
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required Major Major { get; set; }
        [Required]
        public required Level Level { get; set; }
        [Required]
        public required AcceptanceType AcceptanceType { get; set; }

        [ForeignKey(nameof(Staff))]
        public int TeacherId { get; set; }
        [InverseProperty(nameof(Staff.TeacherSubjects))]
        public  Staff? Teacher { get; set; }

        [Required]
        [ForeignKey(nameof(Staff))]
        public int HeadOfScientificDepartmentId { get; set; }
        [InverseProperty(nameof(Staff.HeadOfScientificDepartmentSubjects))]
        public required Staff AssignedBy { get; set; }

        [InverseProperty(nameof(Student.InterestedSubjects))]
        public ICollection<Student>? InterestedStudents { get; set; }
    }
}
