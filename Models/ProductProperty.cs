using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class ProductProperty
    {
        public ProductProperty()
        {
            OrderProperty = new HashSet<OrderProperty>();
        }

        public int ProductPropertyId { get; set; }
        public int? ProductId { get; set; }
        public int? ProPertyId { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public decimal? Price { get; set; }

        public virtual Property ProPerty { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderProperty> OrderProperty { get; set; }
    }
}
