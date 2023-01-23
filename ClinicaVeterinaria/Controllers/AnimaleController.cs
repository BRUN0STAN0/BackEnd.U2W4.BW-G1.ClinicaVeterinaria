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
    [Authorize]
    public class AnimaleController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Animale
        public ActionResult Index()
        {
            var animale = db.Animale.Include(a => a.TipologiaAnimale);
            return View(animale.ToList());
        }

        // GET: Animale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            return View(animale);
        }

        // GET: Animale/Create
        public ActionResult Create()
        {
            ViewBag.ID_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome");
            return View();
        }

        // POST: Animale/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Animale,DataRegistrazione,Nome,ID_TipologiaAnimale,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NominativoProprietario,Smarrito,Foto,DataInizioRicovero")] Animale animale)
        {
            if (ModelState.IsValid)
            {
                db.Animale.Add(animale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.ID_TipologiaAnimale);
            return View(animale);
        }

        // GET: Animale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.ID_TipologiaAnimale);
            return View(animale);
        }

        // POST: Animale/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Animale,DataRegistrazione,Nome,ID_TipologiaAnimale,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NominativoProprietario,Smarrito,Foto,DataInizioRicovero")] Animale animale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.ID_TipologiaAnimale);
            return View(animale);
        }

        // GET: Animale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            return View(animale);
        }

        // POST: Animale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animale animale = db.Animale.Find(id);
            db.Animale.Remove(animale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
