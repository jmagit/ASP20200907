using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Models;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Controllers {
    public class ClientesController : Controller {
        public IActionResult Index() {
            //var srv = new ClienteService();
            //var rslt = srv.GetAll();
            //return Json(rslt);
            var srv = new ClienteService();
            var modelo = srv.GetAll();
            return View("List", modelo);
        }

        public IActionResult List(int? id) {
            var srv = new ClienteService();
            var modelo = srv.GetAll();
            return View(modelo);

            //if (id.HasValue && id.Value == 1) {
            //    var srv = new ClienteService();
            //    var rslt = srv.GetAll();
            //    return Json(rslt);

            //} else {
            //    var srv = new EmpleadoService();
            //    var rslt = srv.GetAll();
            //    return Json(rslt);
            //}
        }
        public IActionResult Details(int id) {
            var srv = new ClienteService();
            var modelo = srv.Get(id);
            if (modelo == null)
                return NotFound();
            return View(modelo);
        }

        public IActionResult Edit(int id) {
            var srv = new ClienteService();
            var modelo = srv.Get(id);
            if (modelo == null)
                return NotFound();
            return View(modelo);
        }

    }
}
