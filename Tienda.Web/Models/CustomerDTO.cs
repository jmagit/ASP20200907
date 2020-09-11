using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.Web.Models {
    public class CustomerDTO {
        [Key]
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
        //[Phone]
        public string Phone { get; set; }

        public static CustomerDTO From(Customer source) {
            return new CustomerDTO {
                CustomerId = source.CustomerId,
                NameStyle = source.NameStyle,
                Title = source.Title,
                FirstName = source.FirstName,
                MiddleName = source.MiddleName,
                LastName = source.LastName,
                Suffix = source.Suffix,
                CompanyName = source.CompanyName,
                SalesPerson = source.SalesPerson,
                EmailAddress = source.EmailAddress,
                Phone = source.Phone
            };
        }
        public static Customer From(CustomerDTO source) {
            return new Customer {
                CustomerId = source.CustomerId,
                NameStyle = source.NameStyle,
                Title = source.Title,
                FirstName = source.FirstName,
                MiddleName = source.MiddleName,
                LastName = source.LastName,
                Suffix = source.Suffix,
                CompanyName = source.CompanyName,
                SalesPerson = source.SalesPerson,
                EmailAddress = source.EmailAddress,
                Phone = source.Phone
            };
        }
        public void To(Customer target) {
            target.CustomerId = CustomerId;
            target.NameStyle = NameStyle;
            target.Title = Title;
            target.FirstName = FirstName;
            target.MiddleName = MiddleName;
            target.LastName = LastName;
            target.Suffix = Suffix;
            target.CompanyName = CompanyName;
            target.SalesPerson = SalesPerson;
            target.EmailAddress = EmailAddress;
            target.Phone = Phone;
        }

    }
}
