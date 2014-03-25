using System.Linq;
using JobScrapping.Data.Entities;

namespace JobScrapping.Data
{
    interface IScrappingRepository
    {
        IQueryable<ScrappingField> GetScrappingFields();
        
        IQueryable<ScrappingDefinitionEntry> GetScrappingDefinitionEntries();
        ScrappingDefinitionEntry GetScrappingDefinitionEntry(int scrappingDefinitionEntryId);
        bool InsertScrappingDefinitionEntry(ScrappingDefinitionEntry entry);
        bool UpdateScrappingDefinitionEntry(ScrappingDefinitionEntry originalEntry,
            ScrappingDefinitionEntry updatedEntry);
        bool DeleteScrappingDefinitionEntry(int scrappingDefinitionEntryId);

        bool SaveAll();
    }
}
