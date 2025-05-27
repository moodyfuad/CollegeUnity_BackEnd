using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.StudentFeatures
{
    public class GStudentDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required Gender Gender { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required AccountStatus AccountStatus { get; set; }
        public string? AccountStatusReason { get; set; }
        public string? ProfilePicturePath { get; set; }
        public required string CardId { get; set; }
        public required string CardIdPicturePath { get; set; }
        public Major Major { get; set; }
        public Level Level { get; set; }
        public AcceptanceType AcceptanceType { get; set; }
        public bool IsLevelEditAble { get; set; }
    }
}
