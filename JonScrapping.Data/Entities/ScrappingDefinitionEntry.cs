using System;
using System.Collections.Generic;

namespace JobScrapping.Data.Entities
{
    public class ScrappingDefinitionEntry
    {
        public int ScrappingDefinitionEntryId { get; set; }
        public int ScrappingSiteId { get; set; }
        public int EntryUserId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ScrappingSite ScrappingSite { get; set; }
        public virtual User User { get; set; }
        public virtual IList<ScrappingFieldDefinition> ScrappingFieldDefinitions { get; set; }
    }
}