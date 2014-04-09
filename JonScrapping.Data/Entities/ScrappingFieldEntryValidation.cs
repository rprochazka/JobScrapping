using JobScrapping.Data.Enums;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// store validation result of a particular field
    /// meaning if the xpath of the particular field gives the expected result
    /// </summary>
    public class ScrappingFieldEntryValidation
    {
        public int ScrappingFieldEntryValidationId { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public int? FailedValidationReasonId { get; set; }
        public int ScrappingFieldEntryId { get; set; }
        public int ScrappingValidationId { get; set; }
        
        public virtual ScrappingFieldEntry ScrappingFieldEntry { get; set; }
        public virtual FailedValidationReason FailedValidationReason { get; set; }
        public virtual ScrappingValidation ScrappingDefinitionValidation { get; set; }
    }
}