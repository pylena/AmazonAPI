using AmazonAPI.Data;
using AmazonAPI.Dto;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AmazonAPI.Repositories
{
    public class OrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            
            _configuration = configuration;

        }

        //db connection
        private SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        //Write a Dapper query to fetch a customer’s orders 
        public IEnumerable<CustomerOrderDto> getCustomerOrders()
        {
            using var connection = GetConnection();
            connection.Open();
            var query = @"
                SELECT o.OrderID, o.OrderDate, u.Name 
                FROM Orders o
                JOIN Users u ON o.UserID = u.UserID";

            return connection.Query<CustomerOrderDto>(query).ToList();
        }


    }
}
