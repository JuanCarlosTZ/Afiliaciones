using AFILIADOS_JUAN_CARLOS_TEJEDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFILIADOS_JUAN_CARLOS_TEJEDA.Controllers
{
    public class AfiliadoController : Controller
    {
        // GET: Afiliado
        public ActionResult Index()
        {            
            Conexion conexion = new Conexion();
            var datos =  conexion.ObtenerAfiliados().ToList();
            return View(datos);
        }

        // GET: Afiliado/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Afiliado/Create
        public ActionResult Create()
        {
            Conexion conexion = new Conexion();
            conexion.OpenConection();
            var planes = conexion.ObtenerPlanes().ToList();
            ViewBag.Planes = planes;
            var estatus = conexion.ObtenerEstatus().ToList();
            ViewBag.Estatus = estatus;
            return View();
        }

        // POST: Afiliado/Create
        [HttpPost]
        public ActionResult Create([Bind] Afiliados afiliado)
        {
            try
            { 
                Conexion conexion = new Conexion();
                    var inserted = conexion.AfiliadosInsert(afiliado);

                    if (inserted) return RedirectToAction("Index");

                if (ModelState.IsValid)
                {
                   
                }
                return View();

            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Afiliado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Afiliado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Afiliado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Afiliado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
