using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobScrapping.Web.Models
{
    public class ScrappingSite
    {
        public int ScrappingSiteId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string AmazonId { get; set; }
    }

    public class ScrappingField
    {
        public int ScrappingFieldId { get; set; }
        public string Name { get; set; }
        public ScrappingFieldType Type { get; set; }
        public bool IsRequired { get; set; }
    }

    public class FailedValidationReason
    {
        public int FailedValidationReasonId { get; set; }
        public string Name { get; set; }
    }

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

    public class ScrappingFieldDefinition
    {
        public int ScrappingFieldDefinitionId { get; set; }
        public int ScrappingFieldId { get; set; }
        public string DefinitionValue { get; set; }

        public virtual ScrappingField ScrappingField { get; set; } 
    }

    public class ScrappingFieldDefinitionValidationResult
    {
        [Key]
        public int ScrappingFieldDefinitionId { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public int? FailedValidationReasonId { get; set; }

        public virtual ScrappingFieldDefinition ScrappingFieldDefinition { get; set; }        
        public virtual FailedValidationReason FailedValidationReason { get; set; }
    }

    public enum ValidationResult
    {
        Failed = 0, Ok = 1
    }

    public enum ScrappingFieldType
    {
        List = 0, Detail = 1
    }

    public class ScrappingDefinitionValidation
    {
        public int ScrappingDefinitionValidationId { get; set; }
        public int ScrappingDefinitionEntryId { get; set; }
        
        public int ValidatingUserId { get; set; }
        
        public DateTime ValidationDate { get; set; }

        public virtual ICollection<ScrappingFieldDefinitionValidationResult> ScrappingFieldDefinitionValidationResults { get; set; }
        public virtual User ValidatingUser { get; set; }        
    }    
}