using AmazonAPI.Data;
using AmazonAPI.Models;
using AmazonAPI.Services;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _db;
        private readonly JwtService _jwt;
        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        //Create a /register endpoint to allow users to sign up 
        // anyone can access it
        [HttpPost("register")]
        public IActionResult register([FromBody] User user   )
        {
            if (user == null)
            {
                return BadRequest();
            }
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok("User registered successfully");    

        }
        //Create a /login endpoint to return a JWT token upon successful authentication 
        // any one can access it

        [HttpPost("login")]

        public IActionResult login([FromBody] User request)
        {
            
            var user = _db.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{request.Email}'.");
            }

            if (user.Password != request.Password)
            {
                return Unauthorized("Invalid email or password.");
            }
            var token = _jwt.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [Authorize]  // Requires authentication
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            // represents the currently authenticated user 
            var userEmail = User.Identity?.Name;
            return Ok(new { message = "Profile data", email = userEmail });


        }
    }
}
