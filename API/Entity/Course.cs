using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int Credit { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Question> Questions { get; set; }
        
        public virtual ICollection<PreRequisite> PreRequisites { get; set; }
        public virtual ICollection<PreRequisite> PreRequisiteTo { get; set; }
    }
}
