using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF
{
    public class CollegeUnityDbContext : DbContext
    {
        public CollegeUnityDbContext(DbContextOptions options) : base(options)
        {
        }

        // Define the DbSets here
    }
}
