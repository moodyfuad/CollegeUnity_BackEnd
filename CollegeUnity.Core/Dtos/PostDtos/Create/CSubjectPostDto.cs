using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.PostDtos.Create
{
    /// <summary>
    /// this class for create a subject post
    /// </summary>
    public class CSubjectPostDto : CPostDto
    {
        public int SubjectId { get; set; }
    }
}
