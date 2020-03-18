using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VladNesterTest.Models;
using VladNesterTest.SomeLogic;

namespace VladNesterTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public IDbConnection Connection { get; set; }
        public OrderController(IDbConnection connection)
        {
            Connection = connection;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>(OrderMethods.TakeOrders(Connection as SqlConnection));
            foreach (var order in orders)
            {
                order.OrderedProducts = new List<OrderedProduct>(OrderMethods.GetOrderedProducts(order.Id,Connection as SqlConnection));
            }
            return orders;
        }

        [HttpPost]
        public void PostOrder(Order order)
        {
            OrderMethods.AddProductsInOrder(order, order.OrderedProducts.FirstOrDefault(),Connection as SqlConnection);
        }

        [HttpPut]
        public void ChangeStatus(Order order)
        {

            OrderMethods.ChangeStatus(order, Connection as SqlConnection);
        }
    }
}