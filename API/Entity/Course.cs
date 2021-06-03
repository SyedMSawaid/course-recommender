using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Course
    {
        [Key, Required, MinLength(6)]
        public string CourseId { get; set; }
        [Required, MinLength(8)]
        public string CourseName { get; set; }
        [Required, MinLength(8)]
        public string CourseDescription { get; set; }
        [Required]
        public int Credit { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Question> Questions { get; set; }
        
        public virtual ICollection<PreRequisite> PreRequisites { get; set; }
        public virtual ICollection<PreRequisite> PreRequisiteTo { get; set; }
    }
}
