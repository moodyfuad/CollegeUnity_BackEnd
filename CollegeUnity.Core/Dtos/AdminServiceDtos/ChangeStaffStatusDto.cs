using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AdminServiceDtos
{
    public class ChangeStaffStatusDto
    {
        public AccountStatus AccountStatus { get; set; }
        public string? AccountStatusReason {  get; set; }
    }
}
