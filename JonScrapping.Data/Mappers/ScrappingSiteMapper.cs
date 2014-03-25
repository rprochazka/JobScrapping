using System.ComponentModel.DataAnnotations.Schema;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    internal class ScrappingSiteMapper : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingSite>
    {
        public ScrappingSiteMapper()
        {
            ToTable("ScrappingSites");

            HasKey(c => c.ScrappingSiteId);
            Property(c => c.ScrappingSiteId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ScrappingSiteId).IsRequired();

            Property(c => c.Name).IsRequired();

            Property(c => c.Url).IsRequired();
        }
    }
}
