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
    [Authorize]
    public class MedicamentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medicamentos
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Index()
        {
            var medicamentoes = db.Medicamentos.Include(m => m.Presentacion);
            return View(medicamentoes.ToList());
        }


        // GET: Medicamentos/Details/5
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = db.Medicamentos.Include(m => m.Presentacion).Include(m => m.Ventas).Where(m => m.Id == id).Single();
            if (medicamento == null)
            {
                return HttpNotFound();
            }  
            return View(medicamento);
        }

        // GET: Medicamentos/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.PresentacionId = new SelectList(db.Presentaciones, "Id", "Nombre");
            return View();
        }

        // POST: Medicamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "Id,Nombre,PresentacionId,Precio,Existencia")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                db.Medicamentos.Add(medicamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PresentacionId = new SelectList(db.Presentaciones, "Id", "Nombre", medicamento.PresentacionId);
            return View(medicamento);
        }

        // GET: Medicamentos/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = db.Medicamentos.Find(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.PresentacionId = new SelectList(db.Presentaciones, "Id", "Nombre", medicamento.PresentacionId);
            return View(medicamento);
        }

        // POST: Medicamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "Id,Nombre,PresentacionId,Precio,Existencia")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PresentacionId = new SelectList(db.Presentaciones, "Id", "Nombre", medicamento.PresentacionId);
            return View(medicamento);
        }

        // GET: Medicamentos/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = db.Medicamentos.Find(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            return View(medicamento);
        }

        // POST: Medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicamento medicamento = db.Medicamentos.Find(id);
            db.Medicamentos.Remove(medicamento);
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
