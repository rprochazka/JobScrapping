using System;
using System.Collections.Generic;

namespace JobScrapping.Data.Entities
{
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