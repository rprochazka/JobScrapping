using System.ComponentModel.DataAnnotations.Schema;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    class ScrappingEntryMapper :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingEntry>
    {
        public ScrappingEntryMapper()
        {
            ToTable("ScrappingEntry");
            
            Property(c => c.ScrappingSiteId)
                .IsRequired();

            //defines navigation to the scrapping site
            HasRequired(e => e.ScrappingSite)
                .WithMany(e => e.ScrappingEntries)
                .HasForeignKey(c => c.ScrappingSiteId)
                .WillCascadeOnDelete();

            //defines navigation to the user
            HasRequired(e => e.User)
                .WithMany(e => e.ScrappingEntries)
                .HasForeignKey(c => c.EntryUserId)
                .WillCascadeOnDelete();            

            //defines navigation to the scrapping fields definitions
            //HasRequired(e => e.ScrappingFieldDefinitions);            
        }
    }        
}