using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class OrderProperty
    {
        public int OrderTypeId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductPropertyId { get; set; }
        public int? SupId { get; set; }
        public decimal? SupFare { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductProperty ProductProperty { get; set; }
    }
}
