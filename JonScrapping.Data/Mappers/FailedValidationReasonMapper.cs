﻿using JobScrapping.Data.Entities;

namespace JobScrapping.Data.Mappers
{
    class FailedValidationReasonMapper : 
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<FailedValidationReason>
    {
        public FailedValidationReasonMapper()
        {
            ToTable("LookupFailedValidationReasons");            

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }    
}
