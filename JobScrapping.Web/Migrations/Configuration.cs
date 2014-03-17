using System.Collections.Generic;
using JobScrapping.Web.Models;

namespace JobScrapping.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobScrapping.Web.DAL.ScrappingDefinitionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JobScrapping.Web.DAL.ScrappingDefinitionContext context)
        {
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

            scrappingFields.ForEach(f=> context.ScrappingFields.AddOrUpdate(sf=>sf.Name, f));
            context.SaveChanges();
        }
    }
}
