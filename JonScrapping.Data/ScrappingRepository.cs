using System;
using System.Linq;
using System.Data.Entity;
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

        public IQueryable<ScrappingEntry> GetScrappingDefinitionEntries()
        {
            return _dbContext
                    .ScrappingEntries
                    .Include(e => e.ScrappingFieldEntries)
                    .Include(e => e.ScrappingFieldEntries.Select(d => d.ScrappingField))
                    .AsQueryable();
        }

        public ScrappingEntry GetScrappingDefinitionEntry(int scrappingDefinitionEntryId)
        {
            return
                _dbContext
                    .ScrappingEntries
                    .Include(e => e.ScrappingFieldEntries)
                    .Include(e => e.ScrappingFieldEntries.Select(d => d.ScrappingField))
                    .Include(e=> e.ScrappingSite)
                    .SingleOrDefault(e => e.ScrappingEntryId == scrappingDefinitionEntryId);            
        }

        public bool InsertScrappingDefinitionEntry(ScrappingEntry entry)
        {
            try
            {
                _dbContext.ScrappingEntries.Add(entry);
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

        public bool UpdateScrappingDefinitionEntry(ScrappingEntry originalEntry, ScrappingEntry updatedEntry)
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
                var entity = _dbContext.ScrappingEntries.Find(scrappingDefinitionEntryId);
                if (null != entity)
                {
                    _dbContext.ScrappingEntries.Remove(entity);
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