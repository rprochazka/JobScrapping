using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// one specific entry of the site scrapping
    /// </summary>
    public class ScrappingEntry
    {        
        public int ScrappingEntryId { get; set; }
        public int ScrappingSiteId { get; set; }
        public int EntryUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        public virtual ScrappingSite ScrappingSite { get; set; }

        //[ForeignKey("EntryUserId")]
        public virtual User User { get; set; }
        public virtual ICollection<ScrappingFieldEntry> ScrappingFieldEntries { get; set; }
    }
}