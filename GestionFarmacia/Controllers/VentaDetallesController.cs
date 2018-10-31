using GestionFarmacia.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmacia.Models;

namespace GestionFarmacia.Controllers
{
    public class VentaDetallesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VentaDetalles
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Index()
        {
            var ventaDetalles = db.VentaDetalles.Include(v => v.Medicamento).Include(v => v.Venta);
            return View(ventaDetalles.ToList());
        }

        // GET: VentaDetalles/Details/5
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaDetalle ventaDetalle = db.VentaDetalles.Find(id);
            if (ventaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(ventaDetalle);
        }

        // GET: VentaDetalles/Create
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Create()
        {
            ViewBag.MedicamentoId = new SelectList(db.Medicamentos, "Id", "Nombre");
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Codigo");
            return View();
        }

        // POST: VentaDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Create([Bind(Include = "Id,MedicamentoId,Cantidad,Precio,VentaId")] VentaDetalle ventaDetalle)
        {
            var medicamento = db.Medicamentos.Where(m => m.Id == ventaDetalle.MedicamentoId).Single();

            if (medicamento.Existencia <= 0 || medicamento.Existencia < ventaDetalle.Cantidad ) {

                TempData["error"] = $"La existencia ({medicamento.Existencia}), es menor a la cantidad solicitada({ventaDetalle.Cantidad})";

                ViewBag.MedicamentoId = new SelectList(db.Medicamentos, "Id", "Nombre", ventaDetalle.MedicamentoId);
                ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Codigo", ventaDetalle.VentaId);
                return RedirectToAction("Details", "Ventas", new { @id = ventaDetalle.VentaId});

            }

            if (ModelState.IsValid) {
                db.VentaDetalles.Add(ventaDetalle);
                db.SaveChanges();
                ReducirExitencia(medicamento, ventaDetalle);
                return RedirectToAction("Details", "Ventas", new { @id = ventaDetalle.VentaId });
                
            }


            ViewBag.MedicamentoId = new SelectList(db.Medicamentos, "Id", "Nombre", ventaDetalle.MedicamentoId);
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Codigo", ventaDetalle.VentaId);
            return View(ventaDetalle);      
        }

        private void ReducirExitencia(Medicamento medicamento, VentaDetalle ventaDetalle)
        {
            medicamento.Existencia = medicamento.Existencia - ventaDetalle.Cantidad;
            db.Entry(medicamento).State = EntityState.Modified;
            db.SaveChanges();
        }

        // GET: VentaDetalles/Edit/5
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaDetalle ventaDetalle = db.VentaDetalles.Find(id);
            if (ventaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicamentoId = new SelectList(db.Medicamentos, "Id", "Nombre", ventaDetalle.MedicamentoId);
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Codigo", ventaDetalle.VentaId);
            return View(ventaDetalle);
        }

        // POST: VentaDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Edit([Bind(Include = "Id,MedicamentoId,Cantidad,Precio,VentaId")] VentaDetalle ventaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicamentoId = new SelectList(db.Medicamentos, "Id", "Nombre", ventaDetalle.MedicamentoId);
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Codigo", ventaDetalle.VentaId);
            return View(ventaDetalle);
        }

        // GET: VentaDetalles/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaDetalle ventaDetalle = db.VentaDetalles.Find(id);
            if (ventaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(ventaDetalle);
        }

        // POST: VentaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            VentaDetalle ventaDetalle = db.VentaDetalles.Find(id);
            db.VentaDetalles.Remove(ventaDetalle);
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
