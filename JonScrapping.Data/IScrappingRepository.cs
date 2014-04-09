using System;
using System.Linq;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data
{
    public interface IScrappingRepository : IDisposable
    {
        IQueryable<ScrappingField> GetScrappingFields();

        ScrappingSite GetScrappingSiteByUrl(string url);
        bool InsertScrappingSite(ScrappingSite scrappingSite);

        User GetUserByAmazonId(string amazonId);
        bool InsertUser(User user);        
        
        IQueryable<ScrappingEntry> GetScrappingDefinitionEntries();
        ScrappingEntry GetScrappingDefinitionEntry(int scrappingDefinitionEntryId);
        bool InsertScrappingDefinitionEntry(ScrappingEntry entry);
        bool UpdateScrappingDefinitionEntry(ScrappingEntry originalEntry,
            ScrappingEntry updatedEntry);
        bool DeleteScrappingDefinitionEntry(int scrappingDefinitionEntryId);

        bool SaveAll();
    }
}
