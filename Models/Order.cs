using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProperty = new HashSet<OrderProperty>();
            Payment = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? OrderDeadline { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProperty> OrderProperty { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
