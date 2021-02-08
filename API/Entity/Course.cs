using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public ICollection<Topic> TopicList { get; set; }
        public ICollection<Student> EnrolledStudents { get; set; }
    }
}
