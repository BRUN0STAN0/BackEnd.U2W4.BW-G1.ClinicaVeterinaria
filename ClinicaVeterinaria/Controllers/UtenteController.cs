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
    public class UtenteController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        [Authorize]
        // GET: Utente
        public ActionResult Index()
        {
            return View(db.Utente.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Utente u)
        {
            if (ModelState.IsValid && db.Utente.Where(x=>x.Username == u.Username && x.Psw == x.Psw).Count() == 1)
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

        [Authorize]
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
        [Authorize]
        // GET: Utente/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Utente/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Utente,Username,Psw")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Utente.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utente);
        }
        [Authorize]
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
            return View(utente);
        }
        [Authorize]
        // POST: Utente/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Utente,Username,Psw")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utente);
        }
        [Authorize]
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
        [Authorize]
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
