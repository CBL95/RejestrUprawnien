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
    public class FirmaController : Controller
    {
        private RejestrEntities db = new RejestrEntities();

        // GET: Firma
        public ActionResult Index()
        {
            return View(db.Firmas.ToList());
        }

        // GET: Firma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // GET: Firma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Firma/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa")] Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Firmas.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index","Widok");
            }

            return View(firma);
        }

        // GET: Firma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // POST: Firma/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa")] Firma firma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firma);
        }

        // GET: Firma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firma firma = db.Firmas.Find(id);
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // POST: Firma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Firma firma = db.Firmas.Find(id);
            db.Firmas.Remove(firma);
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
