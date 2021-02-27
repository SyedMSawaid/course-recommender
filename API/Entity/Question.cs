using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string Query { get; set; }
        
        public ICollection<Reply> Replies { get; set; }
        
        public string CourseId { get; set; }
        public Course Course { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
