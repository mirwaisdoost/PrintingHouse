using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class SubAccount
    {
        public SubAccount()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int SubAccountId { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public decimal? Balance { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
