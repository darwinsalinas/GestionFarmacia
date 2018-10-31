using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmacia.Models;
using GestionFarmacia.Models;

namespace GestionFarmacia.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PresentacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Presentaciones
        public ActionResult Index()
        {
            return View(db.Presentaciones.ToList());
        }

        // GET: Presentaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentacion presentacion = db.Presentaciones.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // GET: Presentaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Presentaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] Presentacion presentacion)
        {
            if (ModelState.IsValid)
            {
                db.Presentaciones.Add(presentacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(presentacion);
        }

        // GET: Presentaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentacion presentacion = db.Presentaciones.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // POST: Presentaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Presentacion presentacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presentacion);
        }

        // GET: Presentaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presentacion presentacion = db.Presentaciones.Find(id);
            if (presentacion == null)
            {
                return HttpNotFound();
            }
            return View(presentacion);
        }

        // POST: Presentaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Presentacion presentacion = db.Presentaciones.Find(id);
            db.Presentaciones.Remove(presentacion);
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
