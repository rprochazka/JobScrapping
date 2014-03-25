using System.Collections.Generic;
using System.Data.Entity.Migrations;
using JobScrapping.Data.Entities;
using JobScrapping.Data.Enums;

namespace JobScrapping.Data
{
    internal class ScrappingContextMigrationConfiguration : DbMigrationsConfiguration<ScrappingContext>
    {
        public ScrappingContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

#if DEBUG
        protected override void Seed(ScrappingContext context)
        {
            //new scrappingDataSeeder(context).Seed();
            //  This method will be called after migrating to the latest version.
            var scrappingFields = new List<ScrappingField>
            {
                new ScrappingField
                {
                    Name = "jobTitleLinkUrl",
                    Type = ScrappingFieldType.List,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "nextPageLinkUrl",
                    Type = ScrappingFieldType.List,
                    IsRequired = true
                },


                new ScrappingField
                {
                    Name = "job title",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "job description",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "company",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "country",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "job ref code",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = false
                },
            };

            scrappingFields.ForEach(f => context.ScrappingFields.AddOrUpdate(sf => sf.Name, f));
            context.SaveChanges();
        }
#endif

    }
}