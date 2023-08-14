using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class ProdductService
    {
        public int ProdductService1 { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Service Service { get; set; }
    }
}
