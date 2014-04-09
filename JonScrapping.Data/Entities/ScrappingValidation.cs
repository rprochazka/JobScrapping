using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// stores validation result of the whole site (all fields included)
    /// </summary>
    public class ScrappingValidation
    {
        public int ScrappingValidationId { get; set; }        
        public int ScrappingEntryId { get; set; }
        public int ValidatingUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ValidationDate { get; set; }

        public virtual ICollection<ScrappingFieldEntryValidation> ScrappingFieldEntryValidations { get; set; }

        //[ForeignKey("ValidatingUserId")]
        public virtual User ValidatingUser { get; set; }
    }
}