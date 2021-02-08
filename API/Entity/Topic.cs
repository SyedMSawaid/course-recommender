using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
