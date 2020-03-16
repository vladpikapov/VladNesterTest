﻿using System;
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
        private IConfiguration Configuration { get; set; }

        public OrderController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>(OrderMethods.TakeOrders(Configuration));
            using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                sql.Open();
                foreach (var order in orders)
                {
                    SqlCommand command = new SqlCommand("SelectOrderProducts", sql);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = order.Id
                    };
                    command.Parameters.Add(parameter);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            order.OrderedProducts.Add(new OrderedProduct { Product = new Product { Id = reader.GetInt32(0), Name = reader.GetString(1), Type = reader.GetString(2), Country = reader.GetString(3) }, CountProduct = reader.GetInt32(4) });

                        }
                        reader.Close();

                    }
                }
            }

            return orders;
        }

        [HttpPost]
        public void PostOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddOrder", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", order.OrdererName);
                command.Parameters.AddWithValue("@StartDate", order.StartDate);
                foreach (var prod in order.OrderedProducts)
                {
                    command.Parameters.AddWithValue("@ProdCount", prod.CountProduct);
                    command.Parameters.AddWithValue("@IdProd", prod.Product.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpPut]
        public void ChangeStatus(Order order)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
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