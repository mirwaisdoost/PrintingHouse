using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Property
    {
        public Property()
        {
            ProductProperty = new HashSet<ProductProperty>();
        }

        public int ProPertyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductProperty> ProductProperty { get; set; }
    }
}
