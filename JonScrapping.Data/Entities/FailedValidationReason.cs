namespace JobScrapping.Data.Entities
{
    /// <summary>
    /// available options for failed validation reasons
    /// </summary>
    public class FailedValidationReason
    {
        public int FailedValidationReasonId { get; set; }
        public string Name { get; set; }
    }
}