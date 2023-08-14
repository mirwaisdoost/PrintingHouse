using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public decimal? PayedAmount { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
