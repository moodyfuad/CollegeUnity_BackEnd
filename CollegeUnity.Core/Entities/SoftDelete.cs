using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public class SoftDelete : ISoftDelete
    {
        public bool IsDelete { get; set; } = false;

        public void Restore()
        {
            IsDelete = false;
        }

        public void Delete()
        {
            IsDelete = true;
        }
    }
}
