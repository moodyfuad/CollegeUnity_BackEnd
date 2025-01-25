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
    public class Staff : User
    {
        [Required]
        public virtual required ICollection<Roles> Roles { get; set; }

        [Required]
        public required EducationDegree EducationDegree { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }

        [InverseProperty(nameof(Subject.Teacher))]
        public virtual ICollection<Subject>? TeacherSubjects {  get; set; } 

        [InverseProperty(nameof(Subject.AssignedBy))]
        public virtual ICollection<Subject>? HeadOfScientificDepartmentSubjects {  get; set; }

        [InverseProperty(nameof(Request.Staff))]
        public virtual ICollection<Request>? StudentRequests { get; set; }

        //[InverseProperty(nameof(Chat.ChatCreator))]
        //public ICollection<Chat>? CreatedChats { get; set; }

    }
}
