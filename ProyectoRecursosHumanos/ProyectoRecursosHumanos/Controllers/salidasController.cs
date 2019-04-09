using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoRecursosHumanos.Models;

namespace ProyectoRecursosHumanos.Controllers
{
    public class salidasController : Controller
    {
        private recursoshumaEntities db = new recursoshumaEntities();

        // GET: salidas
        public ActionResult Index()
        {
            var salidas = db.salidas.Include(s => s.empleados);
            return View(salidas.ToList());
        }

        // GET: salidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salidas salidas = db.salidas.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            return View(salidas);
        }

        // GET: salidas/Create
        public ActionResult Create()
        {
            ViewBag.id_salempleado = new SelectList(db.empleados, "id", "codigo_empleado");
            return View();
        }

        // POST: salidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_salempleado,tipo_salida,motivo,fecha_salida")] salidas salidas)
        {

            if (ModelState.IsValid)
            {

                db.salidas.Add(salidas);
				db.empleados.Find(salidas.id_salempleado).estatus = "Inactivo";
				db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_salempleado = new SelectList(db.empleados, "id", "codigo_empleado", salidas.id_salempleado);
            return View(salidas);
        }

        // GET: salidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salidas salidas = db.salidas.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_salempleado = new SelectList(db.empleados, "id", "codigo_empleado", salidas.id_salempleado);
            return View(salidas);
        }

        // POST: salidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_salempleado,tipo_salida,motivo,fecha_salida")] salidas salidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salidas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_salempleado = new SelectList(db.empleados, "id", "codigo_empleado", salidas.id_salempleado);
            return View(salidas);
        }

        // GET: salidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salidas salidas = db.salidas.Find(id);
            if (salidas == null)
            {
                return HttpNotFound();
            }
            return View(salidas);
        }

        // POST: salidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            salidas salidas = db.salidas.Find(id);
            db.salidas.Remove(salidas);
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
