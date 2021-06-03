using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CourseUpdateDto
    {
        public string CourseId { get; set; }
        public string NewPrimaryKey { get; set; }
        [Required, MinLength(6)]
        public string CourseName { get; set; }
        [Required, MinLength(8)]
        public string CourseDescription { get; set; }
        [Required]
        public int Credit { get; set; }
    }
}