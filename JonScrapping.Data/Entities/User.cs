using System.Collections.Generic;

namespace JobScrapping.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string AmazonId { get; set; }

        public virtual ICollection<ScrappingDefinitionEntry> ScrappingDefinitionEntries { get; set; } 
    }
}