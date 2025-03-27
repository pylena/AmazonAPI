using AmazonAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AmazonAPI.Repositories
{
    public class ProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        //db connection
        private SqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }


        //Write a Dapper query to fetch a product by ID

        public Product? GetProductById(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            var query = "SELECT * FROM Products WHERE ProductID = @id";
            return connection.QuerySingleOrDefault<Product>(query, new { id });
        }



    }
}
