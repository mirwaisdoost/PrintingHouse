using System;
using System.Collections.Generic;

namespace PrintingHouse.Models
{
    public partial class Account
    {
        public Account()
        {
            SubAccount = new HashSet<SubAccount>();
        }

        public int AccountId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SubAccount> SubAccount { get; set; }
    }
}
