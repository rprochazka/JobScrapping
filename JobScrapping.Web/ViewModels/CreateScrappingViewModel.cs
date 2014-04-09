using System.Collections.Generic;

namespace JobScrapping.Web.ViewModels
{
    public class CreateScrappingViewModel
    {
        public ScrappingSiteViewModel Site { get; set; }

        public int SiteId { get; set; }

        public int UserId { get; set; }        

        public IList<ScrappingFieldViewModel> ScrappingFields { get; set; } 
    }

    public class ScrappingSiteViewModel
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
    }    

    public class ScrappingFieldViewModel
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}