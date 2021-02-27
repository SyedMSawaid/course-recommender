using API.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<PreRequisite> PreRequisites { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Replies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            
            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<PreRequisite>()
                .HasOne(pr => pr.PreRequisites)
                .WithMany(pr => pr.PreRequisites)
                .HasForeignKey(m => m.PreRequisitesId);

            modelBuilder.Entity<PreRequisite>()
                .HasOne(pr => pr.PreRequisiteTo)
                .WithMany(t => t.PreRequisiteTo)
                .HasForeignKey(m => m.PreRequisiteToId);
        }
    }
}
