using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    class ScrappingFieldEntryMapper :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingFieldEntry>
    {
        public ScrappingFieldEntryMapper()
        {
            ToTable("ScrappingFieldEntry");            

            Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(255);

            //defines scrappingfield navigation
            //HasRequired(e => e.ScrappingField)
            //    .WithMany(e => e.ScrappingFieldDefinitions)
            //    .HasForeignKey(c => c.ScrappingFieldId);

            HasRequired(e=>e.ScrappingEntry)
                .WithMany(e=>e.ScrappingFieldEntries)
                .HasForeignKey(c=>c.ScrappingEntryId)
                .WillCascadeOnDelete();

            ////defines scrapping definition entry navigation
            //HasOptional(e => e.ScrappingDefinitionEntry)
            //    .WithMany(e => e.ScrappingFieldDefinitions);            

        }
    }
}