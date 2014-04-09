using System.Collections.Generic;
using JobScrapping.Data.Enums;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// one particular field of the site scrapping
    /// the scrapping site entry is made up of several scrapping fields
    /// </summary>
    public class ScrappingField
    {
        public int ScrappingFieldId { get; set; }
        public string Name { get; set; }
        public ScrappingFieldType Type { get; set; }
        public bool IsRequired { get; set; }

        public virtual ICollection<ScrappingFieldEntry> ScrappingFieldDefinitions { get; set; }
    }
}