using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entity
{
    public class PreRequisite
    {
        [Key]
        public string Id { get; set; }

        public string PreRequisitesId { get; set; }
        public string PreRequisiteToId { get; set; }
        
        public virtual Course PreRequisites { get; set; }
        public virtual Course PreRequisiteTo { get; set; }
    }
}