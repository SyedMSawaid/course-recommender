using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Student : AppUser
    {
        public string Contact { get; set; }
        public string Address { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
