using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Suplier
    {
        public Suplier()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int SuplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
