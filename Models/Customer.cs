using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Order> Order { get; set; }

    }
}
