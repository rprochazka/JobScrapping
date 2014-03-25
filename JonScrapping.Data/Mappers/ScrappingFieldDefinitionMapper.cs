using System.ComponentModel.DataAnnotations.Schema;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    internal class ScrappingFieldDefinitionMapper :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingFieldDefinition>
    {
        public ScrappingFieldDefinitionMapper()
        {
            ToTable("ScrappingFieldDefinitions");
            
            Property(c => c.ScrappingFieldDefinitionId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);                            
            
            Property(c => c.DefinitionValue)
                .IsRequired()
                .HasMaxLength(255);

            HasKey(c => c.ScrappingFieldDefinitionId);
            HasKey(c => c.ScrappingFieldId);

            HasRequired(e => e.ScrappingField)
                .WithMany(e => e.ScrappingFieldDefinitions)
                .HasForeignKey(c => c.ScrappingFieldId);
        }
    }
}