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
                SqlCommand command = new SqlCommand("select o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,count(p.ProductName) as ProductCount,p.Id,p.ProductName,p.ProductType,p.Country from ORDERS o inner join ORDERSPRODUCTS op"
                + " on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id" +
                " group by o.Id,p.ProductName,p.ProductType,p.Country,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,p.Id;", sql);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.Id = reader.GetInt32(0);
                        order.OrdererName = reader.GetString(1);
                        order.OrderStatus = reader.GetString(2);
                        order.StartDate = reader.GetDateTime(3);
                        order.EndDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4);
                        order.ProductCount = reader.GetInt32(5);
                        order.Products = new List<Product>();
                        order.Products.Add(new Product { Id = reader.GetInt32(6), Name = reader.GetString(7), Type = reader.GetString(8), Country = reader.GetString(9) });
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }
    }
}