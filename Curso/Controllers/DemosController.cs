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
        public string Saluda() {
            return "Hola mundo";
        }
        public string Despide(string id = "mundo") {
            return $"Adiós {id}";
        }
    }
}
