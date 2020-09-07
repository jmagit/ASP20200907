using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Models {
    public class Empleado {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento {  get; set;}
        public bool Activo { get; private set; } = true;

        public Empleado(int id, string nombre, string apellidos, DateTime? fechaNacimiento = null) {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
        }

        // Funcionalidad de negocio
        public void Eliminar() {
            Activo = false;
        }
    }

    public interface IEmpleadoRepository {
        Empleado Get(int id);
        IEnumerable<Empleado> GetAll();
    }

    public class EmpleadoRepository : IEmpleadoRepository {
        private static IList<Empleado> lista = new List<Empleado>();
        static EmpleadoRepository() {
            lista.Add(new Empleado(1, "Pepito", "Grillo"));
            lista.Add(new Empleado(3, "Capitan", "Tan"));
        }
        public IEnumerable<Empleado> GetAll() {
            return lista;
        }
        public Empleado Get(int id) {
            return lista.FirstOrDefault(o => o.Id == id);
        }
    }

    public class EmpleadoService {
        private IEmpleadoRepository repository = new EmpleadoRepository();
        public IEnumerable<Empleado> GetAll() {
            return repository.GetAll();
        }
        public Empleado Get(int id) {
            return repository.Get(id);
        }
    }

}
