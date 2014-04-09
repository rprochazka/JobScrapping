using System.ComponentModel.DataAnnotations.Schema;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    internal class ScrappingSiteMapper : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingSite>
    {
        public ScrappingSiteMapper()
        {
            ToTable("LookupScrappingSites");            
        }
    }
}
