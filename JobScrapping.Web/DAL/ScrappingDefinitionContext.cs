using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using JobScrapping.Web.Models;

namespace JobScrapping.Web.DAL
{
    public class ScrappingDefinitionContext : DbContext
    {
        public ScrappingDefinitionContext()
            : base("JobScrapping")
        {
        }

        public DbSet<ScrappingSite> ScrappingSites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScrappingField> ScrappingFields { get; set; }
        public DbSet<FailedValidationReason> FailedReasons { get; set;}

        public DbSet<ScrappingDefinitionEntry> ScrappingDefinitionEntries { get; set; }     
        public DbSet<ScrappingDefinitionValidation> ScrappingDefinitionValidations { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            DateTime saveTime = DateTime.Now;
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == System.Data.EntityState.Added))
            {
                if (entry.Property("DateCreated").CurrentValue == null)
                    entry.Property("DateCreated").CurrentValue = saveTime;
            }
            return base.SaveChanges();

        }
    }
}