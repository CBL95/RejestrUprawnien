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
    public class Poziom_uprawnienController : Controller
    {
        private RejestrEntities db = new RejestrEntities();

        // GET: Poziom_uprawnien
        public ActionResult Index()
        {
            var poziom_uprawnien = db.Poziom_uprawnien.Include(p => p.Zasob);
            return View(poziom_uprawnien.ToList());
        }

        // GET: Poziom_uprawnien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poziom_uprawnien poziom_uprawnien = db.Poziom_uprawnien.Find(id);
            if (poziom_uprawnien == null)
            {
                return HttpNotFound();
            }
            return View(poziom_uprawnien);
        }

        // GET: Poziom_uprawnien/Create
        public ActionResult Create()
        {
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa");
            return View();
        }

        // POST: Poziom_uprawnien/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,id_zasob")] Poziom_uprawnien poziom_uprawnien)
        {
            if (ModelState.IsValid)
            {
                db.Poziom_uprawnien.Add(poziom_uprawnien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", poziom_uprawnien.id_zasob);
            return View(poziom_uprawnien);
        }

        // GET: Poziom_uprawnien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poziom_uprawnien poziom_uprawnien = db.Poziom_uprawnien.Find(id);
            if (poziom_uprawnien == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", poziom_uprawnien.id_zasob);
            return View(poziom_uprawnien);
        }

        // POST: Poziom_uprawnien/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,id_zasob")] Poziom_uprawnien poziom_uprawnien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poziom_uprawnien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_zasob = new SelectList(db.Zasobs, "id", "nazwa", poziom_uprawnien.id_zasob);
            return View(poziom_uprawnien);
        }

        // GET: Poziom_uprawnien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poziom_uprawnien poziom_uprawnien = db.Poziom_uprawnien.Find(id);
            if (poziom_uprawnien == null)
            {
                return HttpNotFound();
            }
            return View(poziom_uprawnien);
        }

        // POST: Poziom_uprawnien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poziom_uprawnien poziom_uprawnien = db.Poziom_uprawnien.Find(id);
            db.Poziom_uprawnien.Remove(poziom_uprawnien);
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
