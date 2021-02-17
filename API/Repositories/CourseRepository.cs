using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entity;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CourseRepository : ICoursesRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }
        
        public void Update(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
        }

        public Task<Course> DeleteCourseAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}