using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using JobScrapping.Data.Entities;
using JobScrapping.Data.Mappers;

namespace JobScrapping.Data
{
    public class ScrappingContext : DbContext
    {
        public ScrappingContext()
            : base("JobScrappingConnection")           
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScrappingContext, ScrappingContextMigrationConfiguration>());
        }

        public DbSet<ScrappingSite> ScrappingSites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScrappingField> ScrappingFields { get; set; }
        public DbSet<FailedValidationReason> FailedReasons { get; set; }

        public DbSet<ScrappingEntry> ScrappingEntries { get; set; }
        public DbSet<ScrappingValidation> ScrappingValidations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new FailedValidationReasonMapper())
                .Add(new ScrappingEntryMapper())
                .Add(new ScrappingSiteMapper())
                .Add(new ScrappingFieldEntryMapper())
                .Add(new ScrappingValidationMapper());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }        

        public override int SaveChanges()
        {
            try
            {
                var changedEntries = this.ChangeTracker.Entries().ToList();
                foreach (var entry in changedEntries)
                {
                    Debug.WriteLine("Entry {0} is on state {1}" + entry.State, entry.Entity.ToString());
                }

                
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

                Debug.WriteLine("{0}: {1}", errorMsg, ex.Message);

                return 0;
            }
            

        }        
    }
}
