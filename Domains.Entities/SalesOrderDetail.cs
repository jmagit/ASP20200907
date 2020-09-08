﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities
{
    [Table("SalesOrderDetail", Schema = "SalesLT")]
    public partial class SalesOrderDetail
    {
        [Key]
        [Column("SalesOrderID")]
        public int SalesOrderId { get; set; }
        [Key]
        [Column("SalesOrderDetailID")]
        public int SalesOrderDetailId { get; set; }
        public short OrderQty { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPriceDiscount { get; set; }
        [Column(TypeName = "numeric(38, 6)")]
        public decimal LineTotal { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("SalesOrderDetail")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(SalesOrderId))]
        [InverseProperty(nameof(SalesOrderHeader.SalesOrderDetail))]
        public virtual SalesOrderHeader SalesOrder { get; set; }
    }
}
