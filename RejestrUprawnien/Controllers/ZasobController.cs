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
    public class ZasobController : Controller
    {
        private RejestrEntities db = new RejestrEntities();

        // GET: Zasob
        public ActionResult Index()
        {
            var zasobs = db.Zasobs.Include(z => z.Firma);
            return View(zasobs.ToList());
        }

        // GET: Zasob/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zasob zasob = db.Zasobs.Find(id);
            if (zasob == null)
            {
                return HttpNotFound();
            }
            return View(zasob);
        }

        // GET: Zasob/Create
        public ActionResult Create()
        {
            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa");
            return View();
        }

        // POST: Zasob/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_firma,nazwa")] Zasob zasob)
        {
            if (ModelState.IsValid)
            {
                db.Zasobs.Add(zasob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa", zasob.id_firma);
            return View(zasob);
        }

        // GET: Zasob/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zasob zasob = db.Zasobs.Find(id);
            if (zasob == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa", zasob.id_firma);
            return View(zasob);
        }

        // POST: Zasob/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_firma,nazwa")] Zasob zasob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zasob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_firma = new SelectList(db.Firmas, "id", "nazwa", zasob.id_firma);
            return View(zasob);
        }

        // GET: Zasob/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zasob zasob = db.Zasobs.Find(id);
            if (zasob == null)
            {
                return HttpNotFound();
            }
            return View(zasob);
        }

        // POST: Zasob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zasob zasob = db.Zasobs.Find(id);
            db.Zasobs.Remove(zasob);
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
