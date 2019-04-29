using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrders.ViewModels
{
    public class OrderTotal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public decimal? Total { get; set; }
    }
}