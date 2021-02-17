using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IStudentRepository
    {
        void Update(Student student);
        Task<bool> SaveAllChangesAsync();
        Task<Student> GetStudentsByIdAsync(int id);
        Task<IEnumerable<Student>> GetStudentsAsync();
        void AddStudent(Student student);
        Task<Student> RemoveStudentAsync(int id);
    }
}