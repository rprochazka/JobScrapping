namespace JobScrapping.Data.Entities
{
    public class ScrappingFieldDefinition
    {
        public int ScrappingFieldDefinitionId { get; set; }
        public int ScrappingFieldId { get; set; }
        public string DefinitionValue { get; set; }

        public virtual ScrappingField ScrappingField { get; set; }
    }
}