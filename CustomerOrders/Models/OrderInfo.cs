using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrders.Models
{
    public class OrderInfo
    {
        public int ID { get; set; }
        public int OrderListID { get; set; }
        public int ProductID { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public decimal? Total { get; set; }
        public virtual OrderList OrderList { get; set; }
        public virtual Product Product { get; set; }
    }
}