using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Query { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public int DiscussionBoardId { get; set; }
        public DiscussionBoard DiscussionBoard { get; set; }
    }
}
