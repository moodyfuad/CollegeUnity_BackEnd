using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string LecturerName { get; set; }

        [Required]
        public required DateTime Date { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required]
        public required int AvailableSeats { get; set; }

        [InverseProperty(nameof(Student.RegisteredCourses))]
        public ICollection<Student>? RegisteredStudents { get; set; }
    }
}
