using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServiceHelper.Service_Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string OrderDate { get; set; }
        public string ItemName { get; set; }
        public string ItemCount { get; set; }
        public string ItemPrice { get; set; }
    }
}
