using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VladNesterTest.Models;

namespace VladNesterTest.SomeLogic
{
    public class OrderMethods
    {
        public static List<Order> TakeOrders(string connectionString)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();
                SqlCommand command = new SqlCommand("select * from ORDERS", sql);
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
                            EndDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4)
                        });

                    }
                    reader.Close();
                }
                return orders;
            }
        }

        public static List<OrderedProduct> GetOrderedProducts(int orderId, string connectionString, SqlConnection connection)
        {
            List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
            connection.Open();
            SqlCommand command = new SqlCommand("SelectOrderProducts", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", orderId);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    orderedProducts.Add(new OrderedProduct { Product = new Product { Id = reader.GetInt32(0), Name = reader.GetString(1), Type = reader.GetString(2), Country = reader.GetString(3) }, CountProduct = reader.GetInt32(4) });
                }
                reader.Close();
            }
            connection.Close();
            return orderedProducts;
        }

        public static void AddProductsInOrder(SqlConnection connection , Order order, OrderedProduct product)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("AddOrder", connection);
            
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", order.OrdererName);
            command.Parameters.AddWithValue("@StartDate", order.StartDate);
            command.Parameters.AddWithValue("@ProdCount", product.CountProduct);
            command.Parameters.AddWithValue("@IdProd", product.Product.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
