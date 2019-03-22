using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RejestrUprawnien.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace RejestrUprawnien.Controllers
{
    public class UprawnienieController : Controller
    {
        private RejestrEntities db = new RejestrEntities();

        // GET: Uprawnienie
        public ActionResult Index()
        {
            var uprawnienies = db.Uprawnienies.Include(u => u.Poziom_uprawnien).Include(u => u.Pracownik).Include(u => u.Zasob);
            return View(uprawnienies.ToList());
        }

        // GET: Uprawnienie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uprawnienie uprawnienie = db.Uprawnienies.Find(id);
            if (uprawnienie == null)
            {
                return HttpNotFound();
            }
            return View(uprawnienie);
        }

        // GET: Uprawnienie/Create
        public ActionResult Create()
        {
            ViewBag.id_poziom = new SelectList(db.Poziom_uprawnien, "id", "nazwa");
            ViewBag.id_pracownik = new SelectList(db.Pracowniks, "id", "nazwisko");
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa");
            ViewBag.id_nazwa_zasobu = new SelectList(db.Nazwa_zasobu, "id", "nazwa");
            return View();
        }

        // POST: Uprawnienie/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_pracownik,id_zasob,id_nazwa_zasobu,id_poziom,data_utworzenia,data_zatwierdzenia,data_usunięcia,zatwierdzil,utworzyl,usunal")] Uprawnienie uprawnienie)
        {
            //if (ModelState.IsValid)
            //{
                db.Uprawnienies.Add(uprawnienie);
                uprawnienie.data_utworzenia = DateTime.Now;
                uprawnienie.utworzyl = User.Identity.GetUserName();

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }


                return RedirectToAction("Index");
            //}
            
            //ViewBag.id_poziom = new SelectList(db.Zasobs.Where(g => g.id == i).ToList(), "nazwa", "nazwa");
            ViewBag.id_poziom = new SelectList(db.Poziom_uprawnien, "id", "nazwa", uprawnienie.id_poziom);
            ViewBag.id_pracownik = new SelectList(db.Pracowniks, "id", "nazwisko", uprawnienie.id_pracownik);
      
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", uprawnienie.id_zasob);
            ViewBag.id_nazwa_zasobu = new SelectList(db.Nazwa_zasobu, "id", "nazwa", uprawnienie.id_nazwa_zasobu);
            return View(uprawnienie);
        }

        // GET: Uprawnienie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uprawnienie uprawnienie = db.Uprawnienies.Find(id);
            if (uprawnienie == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.id_poziom = new SelectList(db.Poziom_uprawnien, "id", "nazwa", uprawnienie.id_poziom);
            ViewBag.id_pracownik = new SelectList(db.Pracowniks, "id", "nazwisko", uprawnienie.id_pracownik);
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", uprawnienie.id_zasob);
            ViewBag.id_nazwa_zasobu = new SelectList(db.Nazwa_zasobu, "id", "nazwa", uprawnienie.id_nazwa_zasobu);

            return View(uprawnienie);
        }

        // POST: Uprawnienie/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_pracownik,id_zasob,id_nazwa_zasobu,id_poziom,data_utworzenia,data_zatwierdzenia,data_usunięcia,zatwierdzil,utworzyl,usunal")] Uprawnienie uprawnienie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uprawnienie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_poziom = new SelectList(db.Poziom_uprawnien, "id", "nazwa", uprawnienie.id_poziom);
            ViewBag.id_pracownik = new SelectList(db.Pracowniks, "id", "nazwisko", uprawnienie.id_pracownik);
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", uprawnienie.id_zasob);
            ViewBag.id_nazwa_zasobu = new SelectList(db.Nazwa_zasobu, "id", "nazwa", uprawnienie.id_nazwa_zasobu);

            return View(uprawnienie);
        }

        // GET: Uprawnienie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uprawnienie uprawnienie = db.Uprawnienies.Find(id);
            if (uprawnienie == null)
            {
                return HttpNotFound();
            }
            return View(uprawnienie);
        }

        // POST: Uprawnienie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uprawnienie uprawnienie = db.Uprawnienies.Find(id);
            db.Uprawnienies.Remove(uprawnienie);
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
