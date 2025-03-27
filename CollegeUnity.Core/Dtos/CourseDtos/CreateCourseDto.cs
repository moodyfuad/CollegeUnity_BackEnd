using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class CreateCourseDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string LecturerName { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public string Location { get; set; }
        
        [Required]
        public int AvailableSeats { get; set; }

        public void MapTo(out Course result)
        {
            var course = new Course()
            {
                Name = this.Name,
                Description = this.Description,
                AvailableSeats = this.AvailableSeats,
                Date = this.Date,
                Location = this.Location,
                LecturerName = this.LecturerName,
                RegisteredStudents = []
            };

            result = course;
        }
        
        public static CreateCourseDto MapFrom(Course course)
        {
            var dto = new CreateCourseDto()
            {
                Name = course.Name,
                Description = course.Description,
                AvailableSeats = course.AvailableSeats,
                Date = course.Date,
                Location = course.Location,
                LecturerName = course.LecturerName,
            };

             return dto;
        }
    }
}
