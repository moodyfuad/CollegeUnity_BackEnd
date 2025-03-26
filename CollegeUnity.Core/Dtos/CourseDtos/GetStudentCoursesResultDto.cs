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

namespace CollegeUnity.Core.Dtos.CourseDtos
{
    public class GetStudentCoursesResultDto
    {
        public GetStudentCoursesResultDto(int id, string name, string description, string lecturerName, DateTime date, string location, int availableSeats, int takenSeats, List<RegisteredStudentForCoursesResultDto>? registeredStudents)
        {
            Id = id;
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

        public string Name { get; set; }

        public string Description { get; set; }

        public string LecturerName { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public int AvailableSeats { get; set; }

        public int TakenSeats { get; set; }

        public List<RegisteredStudentForCoursesResultDto>? RegisteredStudents { get; set; }

        public static GetStudentCoursesResultDto MapFrom(Course course)
        {
            int takenSeats = course.RegisteredStudents?.Count ?? 0;
            List<RegisteredStudentForCoursesResultDto>? students =
                course.RegisteredStudents?.Select(s => RegisteredStudentForCoursesResultDto.MapFrom(s)).ToList() ?? null;

            GetStudentCoursesResultDto result = new GetStudentCoursesResultDto(
                id: course.Id,
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

        public static PagedList<GetStudentCoursesResultDto> MapFrom(PagedList<Course>? courses)
        {
            //courses ??= new PagedList<Course>([],0,1,10);
            PagedList<GetStudentCoursesResultDto> result = new(
                items: courses.Select(c => MapFrom(c)).ToList()??[],
                count: courses.TotalCount,
                pageNumber: courses.CurrentPage,
                pageSize: courses.PageSize);

            return result;
        }

    }
}
