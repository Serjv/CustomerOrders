using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrders.Models
{
    public class OrderList
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime? Date { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}