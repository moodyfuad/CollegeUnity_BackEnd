using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetPostCommentsParameters : QueryStringParameters
    {
        [Required(ErrorMessage = "post id can not be empty")]
        public int PostId { get; set; }
    }
}
