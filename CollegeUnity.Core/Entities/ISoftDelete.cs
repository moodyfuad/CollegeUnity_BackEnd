using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Entities
{
    public interface ISoftDelete
    {
        public bool IsDelete { get; set; }
        public void Delete();

        public void Restore();
    }
}
