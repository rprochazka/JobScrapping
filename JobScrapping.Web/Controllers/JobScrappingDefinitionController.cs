using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using JobScrapping.Web.DAL;
using JobScrapping.Web.Models;

namespace JobScrapping.Web.Controllers
{
    public class JobScrappingDefinitionController : Controller
    {
        private ScrappingDefinitionContext db = new ScrappingDefinitionContext();

        //
        // GET: /JobScrappingDefinition/

        public ActionResult Index()
        {
            var scrappingdefinitionentries = db.ScrappingDefinitionEntries.Include(s => s.ScrappingSite);
            return View(scrappingdefinitionentries.ToList());
        }

        //
        // GET: /JobScrappingDefinition/Details/5

        public ActionResult Details(int id = 0)
        {
            ScrappingDefinitionEntry scrappingdefinitionentry = db.ScrappingDefinitionEntries.Find(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }
            return View(scrappingdefinitionentry);
        }

        //
        // GET: /JobScrappingDefinition/Create

        public ActionResult Create()
        {
            //ViewBag.ScrappingSiteId = new SelectList(db.ScrappingSites, "ScrappingSiteId", "Name");

            var scrappingFieldDefinitions =
                db.ScrappingFields
                    .Select(sf =>
                        new ScrappingFieldDefinition
                        {
                            ScrappingFieldId = sf.ScrappingFieldId,
                            ScrappingField = sf
                        });

            var scrappingDefinitionEntry = new ScrappingDefinitionEntry
            {
                ScrappingFieldDefinitions = scrappingFieldDefinitions.ToList()
            };
            return View(scrappingDefinitionEntry);

            //return View();
        }

        //
        // POST: /JobScrappingDefinition/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScrappingDefinitionEntry scrappingdefinitionentry)
        {
            if (ModelState.IsValid)
            {
                db.ScrappingDefinitionEntries.Add(scrappingdefinitionentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScrappingSiteId = new SelectList(db.ScrappingSites, "ScrappingSiteId", "Name", scrappingdefinitionentry.ScrappingSiteId);
            return View(scrappingdefinitionentry);
        }

        //
        // GET: /JobScrappingDefinition/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ScrappingDefinitionEntry scrappingdefinitionentry = db.ScrappingDefinitionEntries.Find(id);
            if (scrappingdefinitionentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScrappingSiteId = new SelectList(db.ScrappingSites, "ScrappingSiteId", "Name", scrappingdefinitionentry.ScrappingSiteId);
            return View(scrappingdefinitionentry);
        }

        //
        // POST: /JobScrappingDefinition/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScrappingDefinitionEntry scrappingdefinitionentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scrappingdefinitionentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScrappingSiteId = new SelectList(db.ScrappingSites, "ScrappingSiteId", "Name", scrappingdefinitionentry.ScrappingSiteId);
            return View(scrappingdefinitionentry);
        }

        //
        // GET: /JobScrappingDefinition/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ScrappingDefinitionEntry scrappingdefinitionentry = db.ScrappingDefinitionEntries.Find(id);
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
            ScrappingDefinitionEntry scrappingdefinitionentry = db.ScrappingDefinitionEntries.Find(id);
            db.ScrappingDefinitionEntries.Remove(scrappingdefinitionentry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}