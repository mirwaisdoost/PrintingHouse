using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime? Date { get; set; }
        public int? SuplierId { get; set; }
        public int? SubAccountId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string Description { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual SubAccount SubAccount { get; set; }
        public virtual Suplier Suplier { get; set; }
    }
}
