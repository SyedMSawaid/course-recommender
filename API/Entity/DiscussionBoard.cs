using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class DiscussionBoard
    {
        public int DiscussionBoardId { get; set; }
        public string CourseId { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
