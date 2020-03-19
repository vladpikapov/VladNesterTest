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
                            orderedProducts.Add(new OrderedProduct
                            {
                                Product = new Product
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Country = reader.GetString(3)
                                },
                                CountProduct = reader.GetInt32(4)
                            });
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
            string sqlCmd = $"insert ORDERS(Orderer,OrderStatus, StartDate, EndDate) values (@Name,'Formation',@StartDate,null)";
            SqlCommand command = new SqlCommand(sqlCmd, connection);
            try
            {
                command.Parameters.AddWithValue("@Name", order.OrdererName);
                command.Parameters.AddWithValue("@StartDate", order.StartDate);
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public static Order GetOrder(Order order, string connectionString)
        {
            List<Order> orders = new List<Order>(GetOrders(connectionString));
            if (orders.Count == 0)
                return null;
            return orders.Where(o => o.OrdererName == order.OrdererName && o.StartDate == order.StartDate).FirstOrDefault();
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

        public static void ChangeStatus(string connectionString, string sqlCmd)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
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

        public static bool CheckProductInOrder(int orderId, int productId, string connectionString)
        {
            List<OrdersProducts> ordersProducts = new List<OrdersProducts>(GetOrdersProducts(connectionString));
            if (ordersProducts.Where(op => op.OrderId == orderId && op.ProductId == productId).Count() != 0)
                return true;
            return false;
        }

        public static void UpdateOrder(int orderId, int productId, int productCount, string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand($"update ORDERSPRODUCTS set CountOrderedProducts += {productCount} where OrdersFK = {orderId} and ProductFK = {productId};", connection);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<OrdersProducts> GetOrdersProducts(string connectionString)
        {
            List<OrdersProducts> ordersProducts = new List<OrdersProducts>();
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from ORDERSPRODUCTS", connection);
            var reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ordersProducts.Add(new OrdersProducts { OrderId = reader.GetInt32(0), ProductId = reader.GetInt32(1), CountProduct = reader.GetInt32(2) });
                    }
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return ordersProducts;
        }
    }
}
