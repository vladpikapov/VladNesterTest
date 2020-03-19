using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VladNesterTest.Models;

namespace VladNesterTest.SomeLogic
{
    public class OrderMethods
    {
        public OrderMethods()
        {
        }

        public static List<Order> GetOrders(string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            List<Order> orders = new List<Order>();
            connection.Open();
            SqlCommand command = new SqlCommand("select * from ORDERS", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                try
                {

                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Id = reader.GetInt32(0),
                            OrdererName = reader.GetString(1),
                            OrderStatus = reader.GetString(2),
                            StartDate = reader.GetDateTime(3),
                            EndDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4)
                        });
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return orders;

        }

        public static List<OrderedProduct> GetOrderedProducts(int orderId, string connectionString)
        {
            List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SelectOrderProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", orderId);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    try
                    {
                        while (reader.Read())
                        {
                            orderedProducts.Add(new OrderedProduct { Product = new Product { Id = reader.GetInt32(0), Name = reader.GetString(1), 
                                Type = reader.GetString(2), Country = reader.GetString(3) }, CountProduct = reader.GetInt32(4) });
                        }
                    }
                    finally
                    {
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            return orderedProducts;
        }

        public static void CreateOrder(Order order, string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlCmd = $"insert ORDERS(Orderer,OrderStatus, StartDate, EndDate) values ('{order.OrdererName}','Formation','{order.StartDate}',null)";
            SqlCommand command = new SqlCommand(sqlCmd,connection);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public static int? GetOrderId(Order order, string connectionString)
        {
            List<Order> orders = new List<Order>(GetOrders(connectionString));
            if (orders.Count == 0)
                return null;
            return orders.Where(o => o.OrdererName == order.OrdererName && o.StartDate == order.StartDate).FirstOrDefault().Id;
        } 

        public static void AddProductsInOrder(int? orderId, OrderedProduct product, string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlCmd = $"insert ORDERSPRODUCTS(OrdersFK,ProductFK,CountOrderedProducts) values ({orderId},{product.Product.Id},{product.CountProduct})";
            SqlCommand command = new SqlCommand(sqlCmd, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public static void ChangeStatus(Order order, string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlCmd = $"update ORDERS set OrderStatus = '{order.OrderStatus}', EndDate = '{order.EndDate}' where Id = {order.Id};";
            SqlCommand command = new SqlCommand(sqlCmd, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
