using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScrapping.Data;
using JobScrapping.Data.Entities;
using JobScrapping.Web.ViewModels;

namespace JobScrapping.Web.Controllers
{
    public class JobScrappingDefinitionController : Controller
    {        
        private readonly IScrappingRepository _repository;
        public JobScrappingDefinitionController(IScrappingRepository respository)
        {
            _repository = respository;
        }

        public JobScrappingDefinitionController() : this (new ScrappingRepository())
        {}

        //
        // GET: /JobScrappingDefinition/

        public ActionResult Index()
        {
            var scrappingdefinitionentries = _repository.GetScrappingDefinitionEntries().ToList();
            return View(scrappingdefinitionentries);
        }

        //
        // GET: /JobScrappingDefinition/Details/5

        public ActionResult Details(int id = 0)
        {
            ScrappingEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }
            return View(scrappingdefinitionentry);
        }
        
        // GET: /JobScrappingDefinition/Create/siteUrl=siteurl&amazonId=1
        public ActionResult Create(string siteUrl, string amazonId)
        {            
            var url = HttpUtility.UrlDecode(siteUrl);
            var selectedSite =
                      _repository.GetScrappingSiteByUrl(siteUrl)
                   ?? new ScrappingSite {Name = url, Url = url};
            if (selectedSite.ScrappingSiteId == 0)
            {
                _repository.InsertScrappingSite(selectedSite);                
            }

            var selectedUser =
                _repository.GetUserByAmazonId(amazonId)
                ?? new User {AmazonId = amazonId};
            if (selectedUser.UserId == 0)
            {
                _repository.InsertUser(selectedUser);
            }
            
            _repository.SaveAll();

            var scrappingFields = _repository.GetScrappingFields().ToList();
            var scrappingFieldEntries =
                scrappingFields
                    .Select(sf =>
                        new ScrappingFieldEntry
                        {
                            ScrappingFieldId = sf.ScrappingFieldId,
                            ScrappingField = sf
                        });          

            var scrappingEntry = new CreateScrappingViewModel
            {
                ScrappingFields = scrappingFieldEntries.ToList()
                                    .Select(e => new ScrappingFieldViewModel
                                    {
                                        FieldId = e.ScrappingFieldId,
                                        FieldName = e.ScrappingField.Name
                                    }).ToList(),
                UserId = selectedUser.UserId,
                Site =
                    new ScrappingSiteViewModel
                    {
                        SiteId = selectedSite.ScrappingSiteId,
                        SiteName = selectedSite.Name,
                        SiteUrl = selectedSite.Url
                    }
            };

            return View(scrappingEntry);            
        }

        //
        // POST: /JobScrappingDefinition/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateScrappingViewModel scrappingEntry)
        {
            if (ModelState.IsValid)
            {
                var modelEntry = new ScrappingEntry
                {
                    EntryUserId = scrappingEntry.UserId,
                    ScrappingSiteId = scrappingEntry.Site.SiteId,
                    ScrappingFieldEntries = 
                        scrappingEntry.ScrappingFields
                            .Select(f => new ScrappingFieldEntry
                            {
                                ScrappingFieldId = f.FieldId,
                                Value = f.FieldValue
                            }).ToArray()
                };

                _repository.InsertScrappingDefinitionEntry(modelEntry);
                _repository.SaveAll();
                
                return RedirectToAction("Index");
            }

            //TODO - load scrapping site here
            return View(scrappingEntry);
        }

        //
        // GET: /JobScrappingDefinition/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ScrappingEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }

            var selectedSite = scrappingdefinitionentry.ScrappingSite;
            var scrappingEntry = new CreateScrappingViewModel
            {
                ScrappingFields = scrappingdefinitionentry.ScrappingFieldEntries.ToList()
                                    .Select(e => new ScrappingFieldViewModel
                                    {
                                        FieldId = e.ScrappingFieldId,
                                        FieldName = e.ScrappingField.Name,
                                        FieldValue = e.Value
                                    }).ToList(),
                UserId = scrappingdefinitionentry.EntryUserId,
                Site =
                    new ScrappingSiteViewModel
                    {
                        SiteId = selectedSite.ScrappingSiteId,
                        SiteName = selectedSite.Name,
                        SiteUrl = selectedSite.Url
                    }
            };

            return View(scrappingEntry);
        }

        //
        // POST: /JobScrappingDefinition/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(ScrappingDefinitionEntry scrappingdefinitionentry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(scrappingdefinitionentry).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ScrappingSiteId = new SelectList(db.ScrappingSites, "ScrappingSiteId", "Name", scrappingdefinitionentry.ScrappingSiteId);
        //    return View(scrappingdefinitionentry);
        //}

        //
        // GET: /JobScrappingDefinition/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ScrappingEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }
            return View(scrappingdefinitionentry);
        }

        //
        // POST: /JobScrappingDefinition/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //ScrappingDefinitionEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
            _repository.DeleteScrappingDefinitionEntry(id);
            _repository.SaveAll();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}