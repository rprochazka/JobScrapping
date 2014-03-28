using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScrapping.Data;
using JobScrapping.Data.Entities;

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
            var scrappingdefinitionentries = _repository.GetScrappingDefinitionEntries();
            return View(scrappingdefinitionentries.ToList());
        }

        //
        // GET: /JobScrappingDefinition/Details/5

        public ActionResult Details(int id = 0)
        {
            ScrappingDefinitionEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }
            return View(scrappingdefinitionentry);
        }
        
        // GET: /JobScrappingDefinition/Create/[siteUrl]
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
            var scrappingFieldDefinitions =
                scrappingFields
                    .Select(sf =>
                        new ScrappingFieldDefinition
                        {
                            ScrappingFieldId = sf.ScrappingFieldId,
                            ScrappingField = sf
                        });

            var scrappingDefinitionEntry = new ScrappingDefinitionEntry
            {
                ScrappingSiteId = selectedSite.ScrappingSiteId,
                ScrappingSite = selectedSite,
                EntryUserId = selectedUser.UserId,
                User = selectedUser,
                ScrappingFieldDefinitions = scrappingFieldDefinitions.ToList()
            };
            return View(scrappingDefinitionEntry);            
        }

        //
        // POST: /JobScrappingDefinition/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScrappingDefinitionEntry scrappingdefinitionentry)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertScrappingDefinitionEntry(scrappingdefinitionentry);
                _repository.SaveAll();
                
                return RedirectToAction("Index");
            }

            ViewBag.ScrappingSiteId = new SelectList(_repository.GetScrappingFields(), "ScrappingSiteId", "Name", scrappingdefinitionentry.ScrappingSiteId);
            return View(scrappingdefinitionentry);
        }

        //
        // GET: /JobScrappingDefinition/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ScrappingDefinitionEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScrappingSiteId = new SelectList(_repository.GetScrappingFields(), "ScrappingSiteId", "Name", scrappingdefinitionentry.ScrappingSiteId);
            return View(scrappingdefinitionentry);
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
            ScrappingDefinitionEntry scrappingdefinitionentry = _repository.GetScrappingDefinitionEntry(id);
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