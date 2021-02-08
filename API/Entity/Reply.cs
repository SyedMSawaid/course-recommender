using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
