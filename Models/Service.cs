using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Service
    {
        public Service()
        {
            ProdductService = new HashSet<ProdductService>();
        }

        public int ServiceId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProdductService> ProdductService { get; set; }
    }
}
