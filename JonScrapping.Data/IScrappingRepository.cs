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
        
        IQueryable<ScrappingDefinitionEntry> GetScrappingDefinitionEntries();
        ScrappingDefinitionEntry GetScrappingDefinitionEntry(int scrappingDefinitionEntryId);
        bool InsertScrappingDefinitionEntry(ScrappingDefinitionEntry entry);
        bool UpdateScrappingDefinitionEntry(ScrappingDefinitionEntry originalEntry,
            ScrappingDefinitionEntry updatedEntry);
        bool DeleteScrappingDefinitionEntry(int scrappingDefinitionEntryId);

        bool SaveAll();
    }
}
