using AmazonAPI.Data;
using AmazonAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;


        }
        // GET /api/products : List all available products
        [HttpGet]
        public IActionResult Products() {
            //only retrieve available products where stock  > 0 
            var products = _db.Products.Where(p => p.Stock > 0).ToList();

            return products.Any() ? Ok(products) : NotFound();

        }


        [HttpPost]
        [Authorize(Roles = "Admin")] // only admin can access it
        public IActionResult addProduct([FromBody] Product product  )
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return Ok("Product added Successfully ");
        }

        //Create an endpoint /products to list all available products
        [HttpGet]
        public IActionResult getProducts()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }







    }
}
