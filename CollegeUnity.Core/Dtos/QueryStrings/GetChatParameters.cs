using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.QueryStrings
{
    public class GetChatParameters : QueryStringParameters
    {
        public string? SearchName { get; set; }
        public bool isEnabled { get; set; }
        public bool isHidden { get; set; }
    }
}
