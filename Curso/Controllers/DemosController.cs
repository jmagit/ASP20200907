using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Controllers {
    public class DemosController : Controller {
        public string Index() {
            return "Esto es una demo";
        }
        //public string Saluda() {
        //    return "Hola mundo";
        //}
        //public string Despide(string id = "mundo") {
        //    return $"Adiós {id}";
        //}

        public IActionResult Saluda() {
            return View();
        }
        public IActionResult Despide(string id = "mundo") {
            if (id == "mundo") {
                return View("Saluda");
            }
            ViewData["Nombre"] = id;
            //ViewData["Pinta"] = true;
            ViewBag.Pinta = true;
            return View();
        }

        [ActionName("Otro")]
        public string Cotilla() {
            return "Cotilla";

        }

    }
}
