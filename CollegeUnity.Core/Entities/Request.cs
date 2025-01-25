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
    public class Request
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Content { get; set; }
        
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Sent;

        [Required]
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        [Required]
        [InverseProperty(nameof(Student.Requests))]
        public required Student RequestedBy { get; set; }

        [Required]
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        [Required]
        [InverseProperty(nameof(Staff.StudentRequests))]
        public required Staff RequestedTo { get; set; }


    }
}
