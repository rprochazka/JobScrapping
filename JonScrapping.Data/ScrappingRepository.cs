using System;
using System.Linq;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data
{
    public class ScrappingRepository : IScrappingRepository
    {
        private readonly ScrappingContext _dbContext;
        public ScrappingRepository(ScrappingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ScrappingRepository() : this(new ScrappingContext())
        {}

        public IQueryable<ScrappingField> GetScrappingFields()
        {
            return _dbContext.ScrappingFields.AsQueryable();
        }

        public ScrappingSite GetScrappingSiteByUrl(string url)
        {
            return _dbContext.ScrappingSites.SingleOrDefault(s => s.Url.Equals(url, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool InsertScrappingSite(ScrappingSite scrappingSite)
        {
            try
            {
                _dbContext.ScrappingSites.Add(scrappingSite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetUserByAmazonId(string amazonId)
        {
            return _dbContext.Users.SingleOrDefault(u => u.AmazonId == amazonId);
        }

        public bool InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            return true;
        }

        public IQueryable<ScrappingDefinitionEntry> GetScrappingDefinitionEntries()
        {
            return _dbContext
                .ScrappingDefinitionEntries
                .Include("ScrappingFieldDefinitions")
                .AsQueryable();
        }

        public ScrappingDefinitionEntry GetScrappingDefinitionEntry(int scrappingDefinitionEntryId)
        {
            return
                _dbContext
                    .ScrappingDefinitionEntries
                    .Include("ScrappingFieldDefinitions")                    
                    .SingleOrDefault(e => e.ScrappingDefinitionEntryId == scrappingDefinitionEntryId);
            //.Find(scrappingDefinitionEntryId);
        }

        public bool InsertScrappingDefinitionEntry(ScrappingDefinitionEntry entry)
        {
            try
            {
                _dbContext.ScrappingDefinitionEntries.Add(entry);
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

        public bool UpdateScrappingDefinitionEntry(ScrappingDefinitionEntry originalEntry, ScrappingDefinitionEntry updatedEntry)
        {
            try
            {
                _dbContext.Entry(originalEntry).CurrentValues.SetValues(updatedEntry);
                return true;
            }
            catch (Exception)
            {
                return true;
            }            
        }

        public bool DeleteScrappingDefinitionEntry(int scrappingDefinitionEntryId)
        {
            try
            {
                var entity = _dbContext.ScrappingDefinitionEntries.Find(scrappingDefinitionEntryId);
                if (null != entity)
                {
                    _dbContext.ScrappingDefinitionEntries.Remove(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public bool SaveAll()
        {
            return _dbContext.SaveChanges() > 0;
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}