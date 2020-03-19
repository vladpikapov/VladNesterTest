using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VladNesterTest.Models
{
    public class OrdersProducts
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CountProduct { get; set; }
    }
}
