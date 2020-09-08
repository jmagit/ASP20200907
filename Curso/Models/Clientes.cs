using Curso.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Models {
    [Table("Clientes")]
   public class Cliente {
        [Required]
        [Column("IdCliente")]
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Apellidos { get; set; }
        [Display(Name ="F.Nacimiento"), DataType(DataType.Date)]
        public DateTime? FechaNacimiento {  get; set;}
        public bool EsEmpleado { get; set; } = false;
        public bool Activo { get; set; } = true;

        public Cliente() { }

        public Cliente(int id, string nombre, string apellidos, DateTime? fechaNacimiento = null, bool esEmpleado = false) {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNacimiento = fechaNacimiento;
            EsEmpleado = esEmpleado;
        }

        // Funcionalidad de negocio
        public void Eliminar() {
            Activo = false;
        }
    }

    public interface IClienteRepository {
        Cliente Get(int id);
        IEnumerable<Cliente> GetAll();
    }

    public class ClienteRepository : IClienteRepository {
        private ApplicationDbContext db = new ApplicationDbContext(null);

        public IEnumerable<Cliente> GetAll() {
            return db.Clientes.ToList();
        }
        public Cliente Get(int id) {
            return db.Clientes.FirstOrDefault(o => o.Id == id);
        }
    }

    public class ClienteMockRepository : IClienteRepository {
        private static IList<Cliente> lista = new List<Cliente>();
        static ClienteMockRepository() {
            lista.Add(new Cliente(1, "Pepito", "Grillo"));
            lista.Add(new Cliente(2, "Carmelo", "Coton"));
            lista.Add(new Cliente(3, "Capitan", "Tan"));
        }
        public IEnumerable<Cliente> GetAll() {
            return lista;
        }
        public Cliente Get(int id) {
            return lista.FirstOrDefault(o => o.Id == id);
        }
    }
    public interface IClienteService {
        Cliente Get(int id);
        IEnumerable<Cliente> GetAll();
    }

    public class ClienteService : IClienteService {
        private IClienteRepository repository = new ClienteRepository();
        public IEnumerable<Cliente> GetAll() {
            return repository.GetAll();
        }
        public Cliente Get(int id) {
            return repository.Get(id);
        }
    }

}
