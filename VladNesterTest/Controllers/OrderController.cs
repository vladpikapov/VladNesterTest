using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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

        public string Connection { get; set; }
        public OrderController(IConfiguration configuration)
        {
            Connection = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>(OrderMethods.GetOrders(Connection));
            foreach (var order in orders)
            {
                order.OrderedProducts = new List<OrderedProduct>(OrderMethods.GetOrderedProducts(order.Id,Connection));
            }
            return orders;
        }

        [HttpPost]
        public void PostOrder(Order order)
        {
            if (order.OrdererName.Length <= 100)
            {
                int? orderId = OrderMethods.GetOrderId(order, Connection);
                if (orderId == null)
                    OrderMethods.CreateOrder(order, Connection);
                OrderedProduct product = order.OrderedProducts.FirstOrDefault();
                orderId = OrderMethods.GetOrderId(order, Connection);
                OrderMethods.AddProductsInOrder(orderId, product, Connection);
                ProductMethods.UpdateProduct(Connection, $"update PRODUCTS set ProductCount -= {product.CountProduct} where Id = {product.Product.Id};");
            }
        }

        [HttpPut]
        public void ChangeStatus(Order order)
        {

            if (order.OrderStatus.Equals("Delivered"))
            {
                order.EndDate = DateTime.Now;
            }
            else
            {
                order.EndDate = null;
            }
            OrderMethods.ChangeStatus(order, Connection);

        }
    }
}