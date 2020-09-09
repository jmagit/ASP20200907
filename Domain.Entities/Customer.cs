using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Entities {
    [Table("Customer", Schema = "SalesLT")]
    public partial class Customer : IValidatableObject {
        public Customer() {
            CustomerAddress = new HashSet<CustomerAddress>();
            SalesOrderHeader = new HashSet<SalesOrderHeader>();
        }

        [Key]
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        public bool NameStyle { get; set; }
        [StringLength(8)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]*\.?$", ErrorMessage = "Debe ser una letra en mayusculas y, opcionalmente, seguida de un punto")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        [StringLength(10)]
        public string Suffix { get; set; }
        [StringLength(128)]
        public string CompanyName { get; set; }
        [StringLength(256)]
        public string SalesPerson { get; set; }
        [StringLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [StringLength(25)]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [StringLength(128)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(10)]
        public string PasswordSalt { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (ModifiedDate.Date.CompareTo(DateTime.Today) == 1)
                yield return new ValidationResult("No puede modificarlo a futuro", new[] { nameof(ModifiedDate) });

        }

        public void CambiaContraseña(string nueva) {
            byte[] salt = new byte[6];
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            rand.GetBytes(salt);
            PasswordSalt = Convert.ToBase64String(salt);
            PasswordHash = GetHashPasswordRfc2898(nueva, PasswordSalt, lenght: 96);
        }
        public bool EsValidaLaContraseña(string introducida) {
            return PasswordHash == GetHashPasswordRfc2898(introducida, PasswordSalt, lenght: 96);
        }
        public string GetHashPasswordRfc2898(string password, string salt, int iterationCount = 100, int lenght = 32) {
            byte[] pwd = Encoding.UTF8.GetBytes(password);
            byte[] _salt = Encoding.UTF8.GetBytes(salt.PadRight(8, '0'));
            var pdb = new Rfc2898DeriveBytes(pwd, _salt, iterationCount);
            return Convert.ToBase64String(pdb.GetBytes(lenght));
        }
    }
}
