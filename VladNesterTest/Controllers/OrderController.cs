using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VladNesterTest.Models;

namespace VladNesterTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }

        public OrderController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                sql.Open();
                SqlCommand command = new SqlCommand("select * from ORDERS o inner join ORDERSPRODUCTS op"
                + " on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id", sql);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Id = reader.GetInt32(0),
                            OrdererName = reader.GetString(1),
                            OrderStatus = reader.GetString(2),
                            StartDate = reader.GetDateTime(3),
                            EndDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                            Product = new Product { Id = reader.GetInt32(7), Name = reader.GetString(8), Type = reader.GetString(9), Country = reader.GetString(10)}
                            
                        });
                    }
                }
            }
            return orders;
        }
    }
}