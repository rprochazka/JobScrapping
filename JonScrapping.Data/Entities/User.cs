using System.Collections.Generic;

namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// list of users using the site
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string AmazonId { get; set; }

        public virtual ICollection<ScrappingEntry> ScrappingEntries { get; set; } 
        public virtual ICollection<ScrappingValidation> ScrappingValidations { get; set; } 
    }
}