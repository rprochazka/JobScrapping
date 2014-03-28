using System.ComponentModel.DataAnnotations.Schema;

namespace JobScrapping.Data.Entities
{
    class ScrappingDefinitionEntryMapper :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingDefinitionEntry>
    {
        public ScrappingDefinitionEntryMapper()
        {
            ToTable("ScrappingDefinitionEntries");

            Property(c => c.ScrappingDefinitionEntryId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.ScrappingSiteId)
                .IsRequired();

            //defines navigation to the scrapping site
            HasRequired(e => e.ScrappingSite)
                .WithMany(e => e.ScrappingDefinitionEntries)
                .HasForeignKey(c => c.ScrappingSiteId);

            //defines navigation to the user
            HasRequired(e => e.User)
                .WithMany(e => e.ScrappingDefinitionEntries)
                .HasForeignKey(c => c.EntryUserId);

            //defines navigation to the scrapping fields definitions
            HasRequired(e => e.ScrappingFieldDefinitions);

            HasKey(c => c.ScrappingDefinitionEntryId);
        }
    }
}