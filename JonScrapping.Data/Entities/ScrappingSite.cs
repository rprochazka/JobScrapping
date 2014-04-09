using System.Collections.Generic;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// list of sites that have been or are planning to be scrapped
    /// </summary>
    public class ScrappingSite
    {
        public int ScrappingSiteId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<ScrappingEntry> ScrappingEntries { get; set; }         
    }
}