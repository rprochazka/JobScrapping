using JobScrapping.Data.Enums;

namespace JobScrapping.Data.Entities
{
    public class ScrappingFieldDefinitionValidationResult
    {
        public int ScrappingFieldDefinitionValidationResultId { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public int? FailedValidationReasonId { get; set; }

        public virtual ScrappingFieldDefinition ScrappingFieldDefinition { get; set; }
        public virtual FailedValidationReason FailedValidationReason { get; set; }
    }
}