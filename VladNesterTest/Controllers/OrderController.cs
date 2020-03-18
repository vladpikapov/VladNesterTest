using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VladNesterTest.Models;
using VladNesterTest.SomeLogic;

namespace VladNesterTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public string ConnectionString { get; set; }

        public OrderController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>(OrderMethods.TakeOrders(ConnectionString));
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            foreach (var order in orders)
            {
                order.OrderedProducts = new List<OrderedProduct>(OrderMethods.GetOrderedProducts(order.Id, sqlConnection));
            }
            return orders;
        }

        [HttpPost]
        public void PostOrder(Order order)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            OrderMethods.AddProductsInOrder(connection, order, order.OrderedProducts.FirstOrDefault());
        }

        [HttpPut]
        public void ChangeStatus(Order order)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ChangeStatus", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", order.Id);
                command.Parameters.AddWithValue("@Status", order.OrderStatus);
                command.ExecuteNonQuery();
            }
        }
    }
}