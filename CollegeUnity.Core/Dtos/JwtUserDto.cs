using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.DomainModels;

namespace CollegeUnity.Core.Dtos
{
    public class JwtUserDto
    {
        public Guid Id { get; set; }

        public required string Username { get; set; }

        public Roles Role { get; set; }
        public required string FirstName { get; set; }

        public required string MiddleName { get; set; }

        public required string LastName { get; set; }

        public Gender Gender { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        public required string Position { get; set; }

        public DateOnly BirthDate { get; set; }

        public required string Email { get; set; }

        public string Password { get; set; }


        public static JwtUserDto DefaultUser()
        {

            return new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mohammed",
                MiddleName = "Fuad",
                LastName = "Bamatraf",
                Username = "moody",
                BirthDate = DateOnly.Parse("1/1/2000"),
                Email = "Email@example.com",
                Password = "1234",
                Position = "Student",
                Role = Roles.Student,
                Gender = Gender.Male
            };
        }


    }


}
