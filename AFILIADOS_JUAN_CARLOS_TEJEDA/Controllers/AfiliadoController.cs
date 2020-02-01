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
            return View();
        }

        // POST: Afiliado/Create
        [HttpPost]
        public ActionResult Create([Bind] Afiliados afiliado)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Conexion conexion = new Conexion();
                    conexion.OpenConection();
                    var inserted = conexion.AfiliadosInsert(afiliado);
                    conexion.CloseConnection();

                    if (inserted) return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
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
