using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class PublicAndBatchPostParameters : QueryStringParameters
    {
        [Required]
        public Major ForMajor { get; set; }
        [Required]
        public Level ForLevel { get; set; }
        [Required]
        public AcceptanceType ForAcceptanceType { get; set; }
    }
}
