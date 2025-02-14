using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SubjectDtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Major Major { get; set; }
        public Level Level { get; set; }
        public AcceptanceType AcceptanceType { get; set; }

        public int? TeacherId { get; set; }

        public int? AssignedById {  get; set; }        
    }
}
