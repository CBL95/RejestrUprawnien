using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RejestrUprawnien.Models;

namespace RejestrUprawnien.Controllers
{
    public class PracownikController : Controller
    {
        private RejestrEntities db = new RejestrEntities();

        // GET: Pracownik
        public ActionResult Index()
        {
            var pracowniks = db.Pracowniks.Include(p => p.Firma);
            return View(pracowniks.ToList());
        }

        // GET: Pracownik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownik pracownik = db.Pracowniks.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            return View(pracownik);
        }

        // GET: Pracownik/Create
        public ActionResult Create()
        {
            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa");
            return View();
        }

        // POST: Pracownik/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imie,nazwisko,id_firma")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                db.Pracowniks.Add(pracownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa", pracownik.id_firma);
            return View(pracownik);
        }

        // GET: Pracownik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownik pracownik = db.Pracowniks.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa", pracownik.id_firma);
            return View(pracownik);
        }

        // POST: Pracownik/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imie,nazwisko,id_firma")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa", pracownik.id_firma);
            return View(pracownik);
        }

        // GET: Pracownik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pracownik pracownik = db.Pracowniks.Find(id);
            if (pracownik == null)
            {
                return HttpNotFound();
            }
            return View(pracownik);
        }

        // POST: Pracownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pracownik pracownik = db.Pracowniks.Find(id);
            db.Pracowniks.Remove(pracownik);
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
