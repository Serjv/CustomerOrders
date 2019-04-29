using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrders.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}