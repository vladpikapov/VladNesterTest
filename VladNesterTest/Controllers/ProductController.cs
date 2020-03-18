using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using VladNesterTest.Models;
using VladNesterTest.SomeLogic;

namespace VladNesterTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        string ConnectionString { get; set; }
        public ProductController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SelectProducts", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
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

        [HttpPost]
        public void AddProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddProduct", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@Country", product.Country);
                command.Parameters.AddWithValue("@Count", product.Count);
                command.ExecuteNonQuery();
            }
        }
        [HttpPut("add/{id}")]
        public void AddOneProduct(int id)
        {
            ProductMethods.AddOrDropeOneProduct("AddOneProduct", id, ConnectionString);
        }

        [HttpPut("drop/{id}")]
        public void DropOneProduct(int id)
        {
            ProductMethods.AddOrDropeOneProduct("DropOneProduct", id, ConnectionString);
        }

    }
}