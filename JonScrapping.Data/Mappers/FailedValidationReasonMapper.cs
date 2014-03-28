using System.ComponentModel.DataAnnotations.Schema;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    class FailedValidationReasonMapper : 
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<FailedValidationReason>
    {
        public FailedValidationReasonMapper()
        {
            ToTable("FailedValidationReasons");

            Property(c => c.FailedValidationReasonId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }    
}
