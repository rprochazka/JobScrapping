using System.ComponentModel.DataAnnotations.Schema;

namespace JobScrapping.Data.Entities
{
    class ScrappingFieldDefinitionMapper :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingFieldDefinition>
    {
        public ScrappingFieldDefinitionMapper()
        {
            ToTable("ScrappingFieldDefinition");

            Property(c => c.ScrappingFieldDefinitionId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.ScrappingFieldId)
                .IsRequired();

            Property(c => c.DefinitionValue)
                .IsRequired()
                .HasMaxLength(255);

            //defines scrappingfield navigation
            HasRequired(e => e.ScrappingField)
                .WithMany(e => e.ScrappingFieldDefinitions)
                .HasForeignKey(c => c.ScrappingFieldId);

            //defines scrapping definition entry navigation
            HasOptional(e => e.ScrappingDefinitionEntry)
                .WithMany(e => e.ScrappingFieldDefinitions);            

        }
    }
}