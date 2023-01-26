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

        [AllowAnonymous]
        public ActionResult CercaByChip()
        {
            return View();
        }
        [AllowAnonymous]
        public JsonResult CercaByChipInput(string microchip)
        {
            Animale animale = db.Animale.Where(x => x.NumeroMicrochip== microchip && x.Smarrito == true).FirstOrDefault();
            if ( animale != null)
            {
                Animale an = new Animale();
                an.Nome = animale.Nome;
                an.DataRegistrazione = animale.DataRegistrazione;
                an.ColoreMantello = animale.ColoreMantello;
                an.DataNascita = animale.DataNascita;
                an.Foto = animale.Foto;
                an.NominativoProprietario = animale.NominativoProprietario;
                an.NumeroMicrochip = animale.NumeroMicrochip;
                an.DataInizioRicovero = animale.DataInizioRicovero;
                an.Smarrito = animale.Smarrito;
                an.Microchip = animale.Microchip;
                return Json(an, JsonRequestBehavior.AllowGet);
            } else
            {
                //an.Nome = "In questa clinica, nessun animale corrisponde a questo chip.";
                return Json("ERROR", JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public ActionResult Ritrovati()
        {
            List<TipologiaAnimale> ListaTipologia = db.TipologiaAnimale.ToList();

            return View(ListaTipologia);
        }
        [AllowAnonymous]
        public JsonResult AnimaliRitrovati(string tipologia)
        {
            List<Animale> listaAnimaliDB = db.Animale.Where(x=> x.TipologiaAnimale.Nome == tipologia && x.Smarrito == true).ToList();
            if ( listaAnimaliDB != null)
            {
                List<Animale> listaAnimali = new List<Animale>();
                foreach (var animale in listaAnimaliDB)
                {

                    Animale an = new Animale();
                    an.Nome = animale.Nome;
                    an.DataRegistrazione = animale.DataRegistrazione;
                    an.ColoreMantello = animale.ColoreMantello;
                    an.DataNascita = animale.DataNascita;
                    an.Foto = animale.Foto;
                    an.NominativoProprietario = animale.NominativoProprietario;
                    an.NumeroMicrochip = animale.NumeroMicrochip;
                    an.DataInizioRicovero = animale.DataInizioRicovero;
                    an.Smarrito = animale.Smarrito;
                    an.Microchip = animale.Microchip;
                    listaAnimali.Add(an);
                }
                return Json(listaAnimali, JsonRequestBehavior.AllowGet);
            }
             else
            {
                return Json("ERROR", JsonRequestBehavior.AllowGet);
            }
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
            TempData["IdAnimale"] = id;
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
        public ActionResult Create(Animale animale, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto == null)
                {
                    animale.Foto = null;
                }
                else
                {

                animale.Foto = Foto.FileName;
                Foto.SaveAs(Server.MapPath("/Content/img/" + Foto.FileName));

                }
                animale.DataRegistrazione = DateTime.Now;
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
            Animale AnimaleInDB = db.Animale.Find(id);
            ViewBag.FotoNomeinDB = AnimaleInDB.Foto;
            ViewBag.ID_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.ID_TipologiaAnimale);
            return View(animale);
        }

        // POST: Animale/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Animale, Nome ,ID_TipologiaAnimale,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NominativoProprietario,Smarrito,Foto,DataInizioRicovero")] Animale animale, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                Animale AnimaleInDB = db.Animale.Find(animale.ID_Animale);
                if (Foto != null)
                {
                    AnimaleInDB.Foto = Foto.FileName;
                    Foto.SaveAs(Server.MapPath("/Content/img/" + Foto.FileName));
                }

                AnimaleInDB.Nome = animale.Nome;
                AnimaleInDB.ColoreMantello = animale.ColoreMantello;
                AnimaleInDB.DataNascita = animale.DataNascita;
                AnimaleInDB.Microchip = animale.Microchip;
                AnimaleInDB.NumeroMicrochip = animale.NumeroMicrochip;
                AnimaleInDB.Smarrito = animale.Smarrito;
                AnimaleInDB.DataInizioRicovero = animale.DataInizioRicovero;



                db.Entry(AnimaleInDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.ID_TipologiaAnimale);
            return View(animale);
        }

        // GET: Animale/Delete/5
        public ActionResult Delete(int? id)
        {            
            Animale animale = db.Animale.Find(id);
            var ListaVisite = db.Visita.Where(x => x.ID_Animale == id).ToList();
            db.Visita.RemoveRange(ListaVisite);
            db.SaveChanges();
            db.Animale.Remove(animale);
            db.SaveChanges();
            return RedirectToAction("Index", db.Animale.ToList());
        }

        // POST: Animale/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Animale animale = db.Animale.Find(id);
        //    db.Animale.Remove(animale);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
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
