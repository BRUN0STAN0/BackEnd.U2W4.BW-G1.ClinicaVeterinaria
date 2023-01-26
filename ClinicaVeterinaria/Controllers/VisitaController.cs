using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class VisitaController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Visita
        public ActionResult Index()
        {
            var visita = db.Visita.Include(v => v.Animale);
            return View(visita.ToList());
        }

        public ActionResult PartialViewIndex(int id)
        {
            List<Visita> ListaVisite = db.Visita.Where(x => x.ID_Animale == id).OrderByDescending(x => x.Data).ToList();
            return PartialView("_PartialViewIndex", ListaVisite);
        }

        // GET: Visita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // GET: Visita/Create
        public ActionResult Create()
        {
            ViewBag.ID_Animale = new SelectList(db.Animale, "ID_Animale", "Nome");
            return View();
        }

        // POST: Visita/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Visita,Data,Descrizione,ID_Animale")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Animale = new SelectList(db.Animale, "ID_Animale", "Nome", visita.ID_Animale);
            return View(visita);
        }

        public ActionResult CreateById(int id)
        {
            ViewBag.ID_Animale = new SelectList(db.Animale, "ID_Animale", "Nome");
            ViewBag.NomeAnimale = db.Animale.Find(id).Nome;

            return View();
        }

        [HttpPost]
        public ActionResult CreateById(Visita visita, int id)
        {
            if (ModelState.IsValid)
            {
                visita.ID_Animale = id;
                db.Visita.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Details/"+ id,"Animale");
            }
            ViewBag.ID_Animale = new SelectList(db.Animale, "ID_Animale", "Nome");
            return View();
        }

        // GET: Visita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Animale = new SelectList(db.Animale, "ID_Animale", "Nome", visita.ID_Animale);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Visita,Data,Descrizione,ID_Animale")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Animale = new SelectList(db.Animale, "ID_Animale", "Nome", visita.ID_Animale);
            return View(visita);
        }

        // GET: Visita/Delete/5
        public ActionResult Delete(int? id)
        {
            Visita visita = db.Visita.Find(id);
            db.Visita.Remove(visita);
            int IdAnimale = Convert.ToInt32(TempData["IdAnimale"]);
            db.SaveChanges();
            return RedirectToAction("Details", "Animale", new { id = IdAnimale });
        }

        // POST: Visita/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Visita visita = db.Visita.Find(id);
        //    db.Visita.Remove(visita);
        //    int IdAnimale = Convert.ToInt32(TempData["IdAnimale"]);
        //    db.SaveChanges();
        //    return RedirectToAction("Details", "Animale", new {IdAnimale});
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
