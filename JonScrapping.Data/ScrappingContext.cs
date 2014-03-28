using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using JobScrapping.Data.Entities;
using JobScrapping.Data.Mappers;

namespace JobScrapping.Data
{
    public class ScrappingContext : DbContext
    {
        public ScrappingContext()
            : base("JobScrapping")
        {
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScrappingContext, ScrappingContextMigrationConfiguration>());
        }

        public DbSet<ScrappingSite> ScrappingSites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScrappingField> ScrappingFields { get; set; }
        public DbSet<FailedValidationReason> FailedReasons { get; set; }

        public DbSet<ScrappingDefinitionEntry> ScrappingDefinitionEntries { get; set; }
        public DbSet<ScrappingDefinitionValidation> ScrappingDefinitionValidations { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations
        //        .Add(new FailedValidationReasonMapper())
        //        .Add(new ScrappingDefinitionEntryMapper())
        //        .Add(new ScrappingSiteMapper())
        //        .Add(new ScrappingFieldDefinitionMapper());

        //    base.OnModelCreating(modelBuilder);
        //}        

        public override int SaveChanges()
        {
            try
            {
                //DateTime saveTime = DateTime.Now;
                //foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
                //{
                //    if (entry.Property("DateCreated").CurrentValue == null)
                //        entry.Property("DateCreated").CurrentValue = saveTime;
                //}
                return base.SaveChanges();
            }

            catch (System.Data.Entity.Validation.DbEntityValidationException dbex)
            {
                string errorMsg = "The action was cancelled due to the following: ";
                foreach (var eve in dbex.EntityValidationErrors)
                {
                    errorMsg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        errorMsg += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);

                    }
                }

                Debug.WriteLine(errorMsg);

                return 0;

            }
            catch (Exception ex)
            {
                string errorMsg = "Database operation error";

                Debug.WriteLine(errorMsg);

                return 0;
            }
            

        }        
    }
}
