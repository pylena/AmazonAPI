using AmazonAPI.Data;
using AmazonAPI.Dto;
using AmazonAPI.Models;
using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrderController(AppDbContext db)
        {
            _db = db;


        }
        
        //Write an eager loading query to fetch orders with their products 

        public IActionResult getAllOrders()
        {
            var orders = _db.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).ToList();

            return Ok(orders);

        }

        //Create an endpoint /orders/create for customers to place an order
        [HttpPost]
        [Authorize(Roles = "Admin")] // only admin can access it
        public IActionResult Create([FromBody] Order order) {
            return Ok();
        }

        //Create an endpoint /orders/{id} to fetch a specific order with details 


        [Authorize(Policy = "CanViewOrdersPolicy")]
        [HttpGet("orders/all")]
        public IActionResult GetAllOrders()
        {
            var orders = _db.Orders.Include(oi => oi.OrderItems).ToList();

            return Ok(orders);
        }
        [HttpPost("refund/{id}")]
        [Authorize(Policy = "CanRefundOrders")]

        public IActionResult requastRefund(int id)
        {
            var order = _db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderID == id);
            if (order == null) { return NotFound($"Unable to load order with number: '{id}'."); }
            order.Status = "Refund";
            _db.SaveChanges();
            return Ok(order);

        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult orderDeatails(int id)
        {
            var order = _db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(order);
        }


    }
}
