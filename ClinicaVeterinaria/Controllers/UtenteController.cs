using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
        [Authorize(Roles="Admin")]
    public class UtenteController : Controller
    {
        private ModelDBContext db = new ModelDBContext();


        // GET: Utente
        public ActionResult Index()
        {
            return View(db.Utente.ToList());
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Utente u)
        {
            if (ModelState.IsValid && db.Utente.Where(x=>x.Username == u.Username && x.Psw == u.Psw).Count() == 1)
            {
                FormsAuthentication.SetAuthCookie(u.Username, true);
                return Redirect(FormsAuthentication.DefaultUrl);
            } else
            {
                ViewBag.Error = "Username e password non coincidono.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        
        // GET: Utente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

     
        // GET: Utente/Create
        public ActionResult Create()
        {
            ViewBag.ID_Ruolo = new SelectList(db.Ruolo, "ID_Ruolo", "Descrizione");
            return View();
        }

        // POST: Utente/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Psw,ID_Ruolo")] Utente utente)
        {
            if (ModelState.IsValid && db.Utente.Where(x => x.Username == utente.Username).Count() == 0)
            {
                //int IdRuolo = db.Ruolo.Where(x => x.Descrizione == "Client").FirstOrDefault().ID_Ruolo; 
                //utente.ID_Ruolo = IdRuolo;
                db.Utente.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.error = "L'username è già utilizzata da un altro utente.";
            ViewBag.ID_Ruolo = new SelectList(db.Ruolo, "ID_Ruolo", "Descrizione");
            return View(utente);
        }




        // GET: Utente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Ruolo = new SelectList(db.Ruolo, "ID_Ruolo", "Descrizione", utente.ID_Ruolo);
            return View(utente);
        }


        // POST: Utente/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Utente,Username,Psw,ID_Ruolo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Ruolo = new SelectList(db.Ruolo, "ID_Ruolo", "Descrizione", utente.ID_Ruolo);
            return View(utente);
        }

        // GET: Utente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Utente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utente utente = db.Utente.Find(id);
            db.Utente.Remove(utente);
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
