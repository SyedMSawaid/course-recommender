using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;
            
            // Adding Courses Data
            var coursesData = await System.IO.File.ReadAllTextAsync("Data/CoursesSeedData.json");
            var courses = JsonSerializer.Deserialize<List<Course>>(coursesData);
            if (courses == null) return;

            foreach (var course in courses)
            {
                await context.Courses.AddAsync(course);
            }
            
            await context.SaveChangesAsync();

            
            // Adding Users Data
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;
            
            var roles = new List<AppRole>
            {
                new AppRole {Name = "Student"},
                new AppRole {Name = "Admin"},
                new AppRole {Name = "Moderator"}
            };
            
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            
            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Student");
            }

            var admin = new AppUser
            {
                UserName = "admin",
            };
            
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
            
            await context.SaveChangesAsync();
            
            
            
            // Adding Enrollment Data
            var enrollmentDataData = await System.IO.File.ReadAllTextAsync("Data/EnrollmentSeedData.json");
            var enrollments = JsonSerializer.Deserialize<List<Enrollment>>(enrollmentDataData);
            if (enrollments == null) return;
            
            foreach (var enrollment in enrollments)
            {
                await context.Enrollments.AddAsync(enrollment);
            }
            
            await context.SaveChangesAsync();
        }
    }
}