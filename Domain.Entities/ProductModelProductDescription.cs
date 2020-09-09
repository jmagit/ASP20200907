﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProductModelProductDescription", Schema = "SalesLT")]
    public partial class ProductModelProductDescription
    {
        [Key]
        [Column("ProductModelID")]
        public int ProductModelId { get; set; }
        [Key]
        [Column("ProductDescriptionID")]
        public int ProductDescriptionId { get; set; }
        [Key]
        [StringLength(6)]
        public string Culture { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey(nameof(ProductDescriptionId))]
        [InverseProperty("ProductModelProductDescription")]
        public virtual ProductDescription ProductDescription { get; set; }
        [ForeignKey(nameof(ProductModelId))]
        [InverseProperty("ProductModelProductDescription")]
        public virtual ProductModel ProductModel { get; set; }
    }
}
