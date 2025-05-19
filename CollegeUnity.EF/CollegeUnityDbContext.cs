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
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Community> Communities  { get; set; }
        public DbSet<CommunityMessage> CommunityMessages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostFile> PostFiles  { get; set; }
        public DbSet<PostVote> PostVotes  { get; set; }
        public DbSet<Request> Requests  { get; set; }
        public DbSet<ScheduleFile> ScheduleFiles  { get; set; }
        public DbSet<Staff> Staffs  { get; set; }
        public DbSet<Student> Students  { get; set; }
        public DbSet<StudentCommunity> StudentCommunities  { get; set; }
        public DbSet<Subject> Subjects  { get; set; }


        // Configure the database using OnModelCreating 
        // DbSets relationships, properties,
        //  methodology to use for creating the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
             .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
            // Configure Community 
            modelBuilder.Entity<Community>()
                .HasMany(com => com.CommunityMessages)
                .WithOne(msg => msg.Community)
                .HasForeignKey(msg => msg.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Community>()
                .HasMany(com => com.CommunityStudents)
                .WithOne(commStu => commStu.Community)
                .HasForeignKey(commStu => commStu.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configure Posts
            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comm => comm.Post)
                .HasForeignKey(comm => comm.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Post>()
                .HasMany(post => post.Votes)
                .WithOne(vote => vote.Post)
                .HasForeignKey(vote => vote.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Post>()
                .HasMany(post => post.PostFiles)
                .WithOne(file => file.Post)
                .HasForeignKey(file => file.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            //Configure chat messages
            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Sender)
                .WithMany()
                .HasForeignKey(cm => cm.SenderId)
                .OnDelete(DeleteBehavior.Cascade); // <== important

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Recipient)
                .WithMany()
                .HasForeignKey(cm => cm.RecipientId)
                .OnDelete(DeleteBehavior.Cascade); // or Restrict, as needed



            modelBuilder.Entity<Post>()
        .HasOne(p => p.Subject)
        .WithMany(s => s.Posts)
        .HasForeignKey(p => p.SubjectId)
        .IsRequired(false); // Make the foreign key not required



            // Configure Users

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Student>().ToTable("Students").HasBaseType<User>();
            modelBuilder.Entity<Staff>().ToTable("Staffs").HasBaseType<User>();

            //modelBuilder.Entity<StudentCommunity>().HasKey(CommunityMember => new { CommunityMember.StudentId, CommunityMember.CommunityId });

            modelBuilder.Entity<ChatMessage>().HasIndex(cm => cm.ChatId);
            modelBuilder.Entity<Student>().HasIndex(st => st.CardId).
                IsUnique(true);

            // chat configuration

            modelBuilder.Entity<Chat>()
            .HasOne(c => c.User1)
            .WithMany(u => u.Chats)
            .HasForeignKey(c => c.User1Id)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany()
                .HasForeignKey(c => c.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Chat and Message relationships
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.ChatMessages)
                .HasForeignKey(m => m.SenderId);

            // configure the Subject 
            //modelBuilder.Entity<Subject>()
            //    .HasOne(sub => sub.Teacher)
            //    .WithMany(teacher => teacher.TeacherSubjects)
            //    .HasForeignKey(sub => sub.TeacherId)
            //    .OnDelete(DeleteBehavior.NoAction);
            
            //modelBuilder.Entity<Subject>()
            //    .HasOne(sub => sub.AssignedBy)
            //    .WithMany(assignedBy => assignedBy.HeadOfScientificDepartmentSubjects)
            //    .HasForeignKey(sub => sub.HeadOfScientificDepartmentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            
            //modelBuilder.Entity<Staff>()
            //    .HasMany(staff => staff.TeacherSubjects)
            //    .WithOne(sub => sub.Teacher)
            //    .HasForeignKey(sub => sub.TeacherId)
            //    .OnDelete(DeleteBehavior.NoAction);
            
            
            //modelBuilder.Entity<Request>()
            //    .HasOne(r => r.Student)
            //    .WithMany(st => st.Requests)
            //    .HasForeignKey(r => r.StudentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            
        }

    }
}
