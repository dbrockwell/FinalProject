using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NorthwindConsole.Model
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        [Required(ErrorMessage = "A name needs to be entered for product")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "A SupplierID needs to be entered for product")]
        public int? SupplierId { get; set; }
        [Required(ErrorMessage = "A CategoryID needs to be entered for product")]
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        [Required(ErrorMessage = "A UnitPrice needs to be entered for product")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
