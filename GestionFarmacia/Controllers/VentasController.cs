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
    //[Authorize]
    public class VentasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ventas
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Index()
        {
            return View(db.Ventas.ToList());
        }

        // GET: Ventas/Details/5
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Venta venta = db.Ventas.Find(id);
            Venta venta = db.Ventas.Include(m => m.VentaDetalles.Select(x => x.Medicamento)).Where(m => m.Id == id).Single();
            ViewBag.total = venta.VentaDetalles.Sum(d => (d.Precio * d.Cantidad));

            if (TempData["error"] != null)
            {
                ViewBag.Error = TempData["error"].ToString();
            }
            

            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicamentoId = new SelectList(db.Medicamentos, "Id", "Nombre");
            ViewBag.VentaId = new SelectList(db.Ventas, "Id", "Codigo");
            venta.Total = ViewBag.total;
            return View(venta);
        }

        // GET: Ventas/Create
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Create([Bind(Include = "Id,Codigo,Fecha,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Ventas.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Details", new { @id = venta.Id});
            }

            return View(venta);
        }

        // GET: Ventas/Edit/5
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Regente")]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Fecha,Total")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venta);
        }

        // GET: Ventas/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
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
