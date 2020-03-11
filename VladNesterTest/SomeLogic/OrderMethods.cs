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
        public static List<Order> TakeOrders(IConfiguration configuration)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection sql = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
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
    }
}
