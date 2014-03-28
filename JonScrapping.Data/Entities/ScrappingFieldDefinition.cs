namespace JobScrapping.Data.Entities
{
    public class ScrappingFieldDefinition
    {
        public int ScrappingFieldDefinitionId { get; set; }
        public int ScrappingFieldId { get; set; }
        public string DefinitionValue { get; set; }

        public ScrappingField ScrappingField { get; set; }
        public ScrappingDefinitionEntry ScrappingDefinitionEntry { get; set; }
    }
}