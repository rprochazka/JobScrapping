using System.Collections.Generic;
using JobScrapping.Data.Enums;

namespace JobScrapping.Data.Entities
{
    public class ScrappingField
    {
        public int ScrappingFieldId { get; set; }
        public string Name { get; set; }
        public ScrappingFieldType Type { get; set; }
        public bool IsRequired { get; set; }

        public virtual ICollection<ScrappingFieldDefinition> ScrappingFieldDefinitions { get; set; }
    }
}