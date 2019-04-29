using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrders.ViewModels
{
    public class CustomerTotal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public decimal? Summa { get; set; }
    }
}