using System;
using System.Collections.Generic;

namespace VladNesterTest.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrdererName { get; set; }
        public string OrderStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
    }
}
