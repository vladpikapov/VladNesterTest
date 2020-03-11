using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladNesterTest.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrdererName { get; set; }
        public string OrderStatus { get; set; }
        public int ProductCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
    }
}
