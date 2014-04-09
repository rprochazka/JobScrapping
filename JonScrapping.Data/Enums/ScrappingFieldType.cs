namespace JobScrapping.Data.Enums
{
    /// <summary>
    /// type of the scrapping field in the context of scrapping
    /// </summary>
    public enum ScrappingFieldType
    {
        /// <summary>
        /// fields in the items list layout - link to detail, paging
        /// </summary>
        List = 0, 
        /// <summary>
        /// fields on the particular detail page where we grab the data
        /// </summary>
        Detail = 1
    }
}
