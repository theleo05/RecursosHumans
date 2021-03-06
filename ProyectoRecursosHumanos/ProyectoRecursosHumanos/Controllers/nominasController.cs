﻿using System;
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
    public class nominasController : Controller
    {
        private recursoshumaEntities db = new recursoshumaEntities();

        // GET: nominas
        public ActionResult Index(string año, string mes)
        {
			var sueldos = from s in db.nominas select s;

			if (!string.IsNullOrEmpty(año))
			{
				sueldos = sueldos.Where(s => s.año == (año));
			}

			if (!string.IsNullOrEmpty(mes))
			{
				sueldos = sueldos.Where(s => s.mes == (mes));
			}
			var nominas = db.nominas.Include(n => n.empleados);
			return View(sueldos);
			
			//var nominas = db.nominas.Include(n => n.empleados);
            //return View(nominas.ToList());
        }

        // GET: nominas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nominas nominas = db.nominas.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // GET: nominas/Create
        public ActionResult Create()
        {
			var z = db.empleados.Where(x => x.estatus == "activo").ToList();
			ViewBag.TotalSalario = z.Sum(a => a.salario);

			//ViewBag.TotalSalario = db.empleados.Sum(a => a.salario);
			db.SaveChanges();
            return View();
        }

        // POST: nominas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,año,mes,monto_total,monto_totalizar")] nominas nominas)
        {
            if (ModelState.IsValid)
            {
                db.nominas.Add(nominas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.monto_total = new SelectList(db.empleados, "id", "codigo_empleado", nominas.monto_total);
            return View(nominas);
        }

        // GET: nominas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nominas nominas = db.nominas.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            ViewBag.monto_total = new SelectList(db.empleados, "id", "codigo_empleado", nominas.monto_total);
            return View(nominas);
        }

        // POST: nominas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,año,mes,monto_total,monto_totalizar")] nominas nominas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nominas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.monto_total = new SelectList(db.empleados, "id", "codigo_empleado", nominas.monto_total);
            return View(nominas);
        }

        // GET: nominas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nominas nominas = db.nominas.Find(id);
            if (nominas == null)
            {
                return HttpNotFound();
            }
            return View(nominas);
        }

        // POST: nominas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            nominas nominas = db.nominas.Find(id);
            db.nominas.Remove(nominas);
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
