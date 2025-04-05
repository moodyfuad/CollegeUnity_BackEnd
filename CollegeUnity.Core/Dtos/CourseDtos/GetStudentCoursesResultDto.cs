using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System.Reflection.Metadata;

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class GetStudentCoursesResultDto
    {
        public GetStudentCoursesResultDto(int id,bool isDeleted, string name, string description, string? lecturerName, DateTime? date, string? location, int? availableSeats, int? takenSeats, List<RegisteredStudentForCoursesResultDto>? registeredStudents)
        {
            Id = id;
            IsDeleted = isDeleted;
            Name = name;
            Description = description;
            LecturerName = lecturerName;
            Date = date;
            Location = location;
            AvailableSeats = availableSeats;
            TakenSeats = takenSeats;
            RegisteredStudents = registeredStudents;
        }

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? LecturerName { get; set; }

        public DateTime? Date { get; set; }

        public string? Location { get; set; }

        public int? AvailableSeats { get; set; }

        public int? TakenSeats { get; set; }

        public List<RegisteredStudentForCoursesResultDto>? RegisteredStudents { get; set; }

        public static GetStudentCoursesResultDto MapFrom(Course course, bool includeStudents, bool hideDeletedDetails = true)
        {
            int takenSeats = course.RegisteredStudents?.Count ?? 0;
            List<RegisteredStudentForCoursesResultDto>? students =
                includeStudents ?
                course.RegisteredStudents?.Select(s => RegisteredStudentForCoursesResultDto.MapFrom(s))
                    .ToList() ?? []
                : null;

            GetStudentCoursesResultDto result =
                course.IsDeleted ?
                new GetStudentCoursesResultDto(
                    id: course.Id,
                    isDeleted: course.IsDeleted,
                    name: course.Name,
                    description: hideDeletedDetails ? "This Course Is Deleted By The Publisher" : course.Description,
                    lecturerName: hideDeletedDetails ? null : course.LecturerName ,
                    date: hideDeletedDetails ? null : course.Date,
                    location: hideDeletedDetails ? null : course.Location,
                    availableSeats: hideDeletedDetails ? null : course.AvailableSeats,
                    takenSeats: hideDeletedDetails ? null : takenSeats,
                    registeredStudents: hideDeletedDetails ? null : students) 
                :
                new GetStudentCoursesResultDto(
                    id: course.Id,
                    isDeleted: course.IsDeleted,
                    name: course.Name,
                    description: course.Description,
                    lecturerName: course.LecturerName,
                    date: course.Date,
                    location: course.Location,
                    availableSeats: course.AvailableSeats,
                    takenSeats: takenSeats,
                    registeredStudents: students);

            return result;
        }

        public static PagedList<GetStudentCoursesResultDto> MapFrom(PagedList<Course> courses, bool includeStudents = false, bool hideDeletedDetails = true)
        {
            
            PagedList<GetStudentCoursesResultDto> result = courses.To(course =>
                MapFrom(course, includeStudents, hideDeletedDetails));
            
            return result;
        }

    }
}
