using API.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        
        public DbSet<DiscussionBoard> DiscussionBoards { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Replies { get; set; }
        
        // Courses Override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.PreRequisites)
                .WithMany(e => e.PreRequisiteTo);
        }
    }
}
