using System.Collections.Generic;

namespace API.DTOs
{
    public class CoursesListDto
    {
        public int StudentId { get; set; }
        public List<string> CoursesList { get; set; }
    }
}