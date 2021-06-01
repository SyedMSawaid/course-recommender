using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PostQuestionDto
    {
        public string Query { get; set; }
        public string CourseId { get; set; }
        public int StudentId { get; set; }
    }
}