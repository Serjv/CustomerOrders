using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrders.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}