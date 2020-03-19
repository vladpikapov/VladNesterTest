using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VladNesterTest.Models;

namespace VladNesterTest.SomeLogic
{
    public class ProductMethods
    {
        public static void AddOrDropeOneProduct(string sqlCmd, string connecionString)
        {
            using SqlConnection connection = new SqlConnection(connecionString);
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

        public static void UpdateProduct(string connectionString, string sqlCmd)
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

        public static void AddProduct(Product product, string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand($"insert into PRODUCTS(ProductName, ProductType, Country, ProductCount) values ('{product.Name}', '{product.Type}', '{product.Country}', '{product.Count}')", connection);
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Product> GetProducts(string connectionString)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM PRODUCTS", connection);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.GetString(2),
                                Country = reader.GetString(3),
                                Count = reader.GetInt32(4)
                            });

                        }
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return products;
        }

        public static Product GetProductById(Product product, string connectionString)
        {
            List<Product> products = new List<Product>(GetProducts(connectionString));
            if (product.Count == 0)
                return null;
            return products.Where(p => p.Name == product.Name && p.Type == product.Type && p.Country == product.Country).FirstOrDefault();
        }
    }
}
