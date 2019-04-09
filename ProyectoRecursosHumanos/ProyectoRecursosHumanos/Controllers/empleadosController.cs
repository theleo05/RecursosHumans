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
    public class empleadosController : Controller
    {
        private recursoshumaEntities db = new recursoshumaEntities();

		// GET: empleados

		public ActionResult Salidaempleado()
		{
			ViewBag.Departamento = new SelectList(db.departamentos, "id", "nombre");
			var empleados = db.empleados.Include(e => e.cargos).Include(e => e.departamentos);
			return View(empleados.ToList());
		}

		[HttpPost]
        public ActionResult Index (string consulta, int departamento)
        {
			////public ActionResult Consulta(int Departamento, string consulta)
			//{
				var empleados = db.empleados.Include(e => e.cargos).Include(e => e.departamentos);
				ViewBag.Departamento = new SelectList(db.departamentos, "id", "nombre");
			if (consulta != null || string.IsNullOrEmpty(consulta) || string.IsNullOrWhiteSpace(departamento.ToString()))
				{
					return View(empleados.Where(x => x.estatus == "Activo" && x.nombre.Contains(consulta) && x.departamento == departamento).ToList());
				}
				else
				{
					return View(empleados.ToList());
				}

			}

			//var nomdep = from n in db.empleados select n;
			
			//if (emp != null)
			//{
			//	nomdep = nomdep.Where(n => n.nombre == emp);
			//}
			
			
        //    //return View(nomdep);
        //}
		[HttpGet]
		public ActionResult Index()
		{
			ViewBag.Departamento = new SelectList(db.departamentos, "id", "nombre");
			var empleados = db.empleados.Include(e => e.cargos).Include(e => e.departamentos);
			return View(empleados.ToList());
		}

        // GET: empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: empleados/Create
        public ActionResult Create()
        {
            ViewBag.cargo = new SelectList(db.cargos, "id", "cargo");
            ViewBag.departamento = new SelectList(db.departamentos, "id", "nombre");
            return View();
        }

        // POST: empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,codigo_empleado,nombre,apellido,telefono,departamento,cargo,fecha_ingreso,salario,estatus")] empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cargo = new SelectList(db.cargos, "id", "cargo", empleados.cargo);
            ViewBag.departamento = new SelectList(db.departamentos, "id", "nombre", empleados.departamento);
            return View(empleados);
        }

        // GET: empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.cargo = new SelectList(db.cargos, "id", "cargo", empleados.cargo);
            ViewBag.departamento = new SelectList(db.departamentos, "id", "nombre", empleados.departamento);
            return View(empleados);
        }

        // POST: empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,codigo_empleado,nombre,apellido,telefono,departamento,cargo,fecha_ingreso,salario,estatus")] empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cargo = new SelectList(db.cargos, "id", "cargo", empleados.cargo);
            ViewBag.departamento = new SelectList(db.departamentos, "id", "nombre", empleados.departamento);
            return View(empleados);
        }

        // GET: empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleados empleados = db.empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleados empleados = db.empleados.Find(id);
            db.empleados.Remove(empleados);
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
