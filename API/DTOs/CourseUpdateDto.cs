namespace API.DTOs
{
    public class CourseUpdateDto
    {
        public string CourseId { get; set; }
        public string NewPrimaryKey { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int Credit { get; set; }
    }
}