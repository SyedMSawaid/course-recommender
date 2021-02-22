using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface ICoursesRepository
    {
        void Update(Course course);
        Task<bool> SaveAllChangesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<IEnumerable<Course>> GetCoursesAsync();
        void AddCourse(Course course);
        Task<Course> DeleteCourseAsync(int id);
        Task<ICollection<Course>> GetPrerequisitesOfCourseAsync(Course course);
        Task<ICollection<Course>> GetPrerequisitesToCourseAsync(Course course);
    }
}