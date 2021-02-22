using System.Collections.Generic;

namespace API.DTOs
{
    public class AddPreRequisiteDto
    {
        public string CourseId { get; set; }
        public ICollection<string> PreRequisitesId { get; set; }
    }
}