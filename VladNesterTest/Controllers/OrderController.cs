using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                order.OrderedProducts = new List<OrderedProduct>(OrderMethods.GetOrderedProducts(order.Id, Connection));
            }
            return orders;
        }

        [HttpPost]
        public void PostOrder(Order order)
        {
            OrderedProduct orderedProduct = order.OrderedProducts.FirstOrDefault();
            if (orderedProduct.Product.Count >= orderedProduct.CountProduct)
            {
                if (order.OrdererName.Length <= 100)
                {
                    var orderFromList = OrderMethods.GetOrder(order, Connection);
                    if (orderFromList == null)
                        OrderMethods.CreateOrder(order, Connection);
                    orderFromList = OrderMethods.GetOrder(order, Connection);
                    if (orderFromList.EndDate == null)
                    {
                        if (OrderMethods.CheckProductInOrder(orderFromList.Id, orderedProduct.Product.Id, Connection))
                        {
                            OrderMethods.UpdateOrder(orderFromList.Id, orderedProduct.Product.Id, orderedProduct.CountProduct, Connection);
                        }
                        else
                        {
                            OrderMethods.AddProductsInOrder(orderFromList.Id, orderedProduct, Connection);
                        }
                        ProductMethods.UpdateProduct(Connection, $"update PRODUCTS set ProductCount -= {orderedProduct.CountProduct} where Id = {orderedProduct.Product.Id};");
                    }
                }
            }
        }

        [HttpPut]
        public void ChangeStatus(Order order)
        {
            StringBuilder sqlCmd = new StringBuilder();
            if (order.OrderStatus == "Delivered")
            {
                order.EndDate = DateTime.Now;
                sqlCmd.Append($"update ORDERS set OrderStatus = '{order.OrderStatus}', EndDate = '{order.EndDate}' where Id = {order.Id};");
            }
            else
            {
                sqlCmd.Append($"update ORDERS set OrderStatus = '{order.OrderStatus}' where Id = {order.Id};");
            }
            OrderMethods.ChangeStatus(Connection, sqlCmd.ToString());

        }
    }
}