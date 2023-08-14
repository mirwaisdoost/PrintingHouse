using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingHouse.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int? ProductID { get; set; }
        public string  ProductName { get; set; }
        public int? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int PropertyID { get; set; }
        public string PropertNyame { get; set; }
        public decimal Price { get; set; }

    }
}
