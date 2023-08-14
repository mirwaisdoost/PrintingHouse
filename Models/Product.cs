using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Product
    {
        public Product()
        {
            ProdductService = new HashSet<ProdductService>();
            ProductProperty = new HashSet<ProductProperty>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProdductService> ProdductService { get; set; }
        public virtual ICollection<ProductProperty> ProductProperty { get; set; }
    }
}
