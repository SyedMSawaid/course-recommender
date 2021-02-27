namespace API.DTOs
{
    public class NewReplyDto
    {
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
    }
}