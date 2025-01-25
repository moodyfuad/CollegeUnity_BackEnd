using CollegeUnity.Core.Entities;
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


        // Configure the database using OnModelCreating 
        // DbSets relationships, properties,
        //  methodology to use for creating the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasIndex().IsUnique();

            modelBuilder.Entity<StudentCommunity>().HasKey(CommunityMember => new { CommunityMember.StudentId, CommunityMember.CommunityId });
        }

    }
}
