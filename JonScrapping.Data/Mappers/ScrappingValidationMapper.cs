using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    class ScrappingValidationMapper :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScrappingValidation>
    {
        public ScrappingValidationMapper()
        {
            ToTable("ScrappingValidation");

            HasRequired(e => e.ValidatingUser)
                .WithMany(e => e.ScrappingValidations)
                .HasForeignKey(c => c.ValidatingUserId)
                .WillCascadeOnDelete();
        }


    }
}
