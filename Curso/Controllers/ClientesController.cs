using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Models;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Controllers {
    public class ClientesController : Controller {
        public IActionResult Index() {
            var srv = new ClienteService();
            var rslt = srv.GetAll();
            return Json(rslt);
        }

        public IActionResult List(int? id) {
            if (id.HasValue && id.Value == 1) {
                var srv = new ClienteService();
                var rslt = srv.GetAll();
                return Json(rslt);

            } else {
                var srv = new EmpleadoService();
                var rslt = srv.GetAll();
                return Json(rslt);
            }
        }

    }
}
