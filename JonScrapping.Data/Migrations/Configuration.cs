using System.Collections.Generic;
using System.Data.Entity;
using JobScrapping.Data.Entities;
using JobScrapping.Data.Enums;

namespace JobScrapping.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ScrappingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;                    
        }

        protected override void Seed(ScrappingContext context)
        {
            //new scrappingDataSeeder(context).Seed();
            //  This method will be called after migrating to the latest version.
            var scrappingFields = new List<ScrappingField>
            {
                new ScrappingField
                {
                    Name = "Job Title Link Url",
                    Type = ScrappingFieldType.List,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "Next Page Link Url",
                    Type = ScrappingFieldType.List,
                    IsRequired = true
                },


                new ScrappingField
                {
                    Name = "Job Title",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "Job Description",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "Company",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "Country",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = true
                },
                new ScrappingField
                {
                    Name = "Job Ref Code",
                    Type = ScrappingFieldType.Detail,
                    IsRequired = false
                },
            };

            scrappingFields.ForEach(f => context.ScrappingFields.AddOrUpdate(sf => sf.Name, f));
            context.SaveChanges();
        }
    }
}
