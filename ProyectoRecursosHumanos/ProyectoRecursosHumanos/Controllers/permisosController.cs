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
    public class permisosController : Controller
    {
        private recursoshumaEntities db = new recursoshumaEntities();

        // GET: permisos
        public ActionResult Index()
        {
            var permisos = db.permisos.Include(p => p.empleados);
            return View(permisos.ToList());
        }

        // GET: permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // GET: permisos/Create
        public ActionResult Create()
        {
            ViewBag.id_perempleado = new SelectList(db.empleados, "id", "codigo_empleado");
            return View();
        }

        // POST: permisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_perempleado,desde,hasta,comentarios")] permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_perempleado = new SelectList(db.empleados, "id", "codigo_empleado", permisos.id_perempleado);
            return View(permisos);
        }

        // GET: permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_perempleado = new SelectList(db.empleados, "id", "codigo_empleado", permisos.id_perempleado);
            return View(permisos);
        }

        // POST: permisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_perempleado,desde,hasta,comentarios")] permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_perempleado = new SelectList(db.empleados, "id", "codigo_empleado", permisos.id_perempleado);
            return View(permisos);
        }

        // GET: permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            permisos permisos = db.permisos.Find(id);
            db.permisos.Remove(permisos);
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
