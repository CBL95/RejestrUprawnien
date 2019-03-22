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
    public class Nazwa_zasobuController : Controller
    {
        private RejestrEntities db = new RejestrEntities();

        // GET: Nazwa_zasobu
        public ActionResult Index()
        {
            var nazwa_zasobu = db.Nazwa_zasobu.Include(n => n.Zasob);
            return View(nazwa_zasobu.ToList());
        }

        // GET: Nazwa_zasobu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nazwa_zasobu nazwa_zasobu = db.Nazwa_zasobu.Find(id);
            if (nazwa_zasobu == null)
            {
                return HttpNotFound();
            }
            return View(nazwa_zasobu);
        }

        // GET: Nazwa_zasobu/Create
        public ActionResult Create()
        {
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa");
            return View();
        }

        // POST: Nazwa_zasobu/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,id_zasob")] Nazwa_zasobu nazwa_zasobu)
        {
            if (ModelState.IsValid)
            {
                db.Nazwa_zasobu.Add(nazwa_zasobu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", nazwa_zasobu.id_zasob);
            return View(nazwa_zasobu);
        }

        // GET: Nazwa_zasobu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nazwa_zasobu nazwa_zasobu = db.Nazwa_zasobu.Find(id);
            if (nazwa_zasobu == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", nazwa_zasobu.id_zasob);
            return View(nazwa_zasobu);
        }

        // POST: Nazwa_zasobu/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,id_zasob")] Nazwa_zasobu nazwa_zasobu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nazwa_zasobu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", nazwa_zasobu.id_zasob);
            return View(nazwa_zasobu);
        }

        // GET: Nazwa_zasobu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nazwa_zasobu nazwa_zasobu = db.Nazwa_zasobu.Find(id);
            if (nazwa_zasobu == null)
            {
                return HttpNotFound();
            }
            return View(nazwa_zasobu);
        }

        // POST: Nazwa_zasobu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nazwa_zasobu nazwa_zasobu = db.Nazwa_zasobu.Find(id);
            db.Nazwa_zasobu.Remove(nazwa_zasobu);
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
