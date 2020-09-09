using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Models;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Controllers {
    public class ClientesController : Controller {
        private readonly IClienteService srv;

        public ClientesController(IClienteService srv) {
            this.srv = srv;
        }

        public IActionResult Index() {
            var modelo = srv.GetAll();
            //return Json(rslt);
            return View("List", modelo);
        }

        public IActionResult List(int? id) {
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
            var modelo = srv.Get(id);
            if (modelo == null)
                return NotFound();
            return View(modelo);
        }

        public IActionResult Edit(int id) {
            var modelo = srv.Get(id);
            if (modelo == null)
                return NotFound();
            return View(modelo);
        }
        [HttpPost]
        public IActionResult Edit(int id, Cliente modelo) {
            return View("Details", modelo);
        }

    }
}
