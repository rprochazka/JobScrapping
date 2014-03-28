using System.Collections.Generic;

namespace JobScrapping.Data.Entities
{
    public class ScrappingSite
    {
        public int ScrappingSiteId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<ScrappingDefinitionEntry> ScrappingDefinitionEntries { get; set; } 
    }
}