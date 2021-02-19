using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entity
{
    public class PreRequisites
    {
        [Key]
        public int PrerequisiteId { get; set; }
        public ICollection<Course> PreRequisistes { get; set; }
        public ICollection<Course> PreRequisiteTo { get; set; }
    }
}