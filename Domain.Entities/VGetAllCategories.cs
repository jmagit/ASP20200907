using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class VGetAllCategories
    {
        [Required]
        [StringLength(50)]
        public string ParentProductCategoryName { get; set; }
        [StringLength(50)]
        public string ProductCategoryName { get; set; }
        [Column("ProductCategoryID")]
        public int? ProductCategoryId { get; set; }
    }
}
