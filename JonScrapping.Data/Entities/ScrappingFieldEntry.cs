using System.ComponentModel.DataAnnotations.Schema;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// entry (xpath value) of a particular field
    /// </summary>
    public class ScrappingFieldEntry
    {
        public int ScrappingFieldEntryId { get; set; }
        public int ScrappingFieldId { get; set; }
        public string Value { get; set; }       
        public int ScrappingEntryId { get; set; }
        
        public virtual ScrappingField ScrappingField { get; set; }
        public virtual ScrappingEntry ScrappingEntry { get; set; }
    }
}