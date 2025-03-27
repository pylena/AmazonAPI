using AmazonAPI.Dto;
using AmazonAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;

        }


        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // Assign UserID and convert it to string
            new Claim(ClaimTypes.Email, user.Email), // Assign email
            new Claim(ClaimTypes.Role, user.Role.ToString()), // Assign role and Convert Role to string

            new Claim("CanViewOrders", "true") // Add the claim to view all orders

        };
            //custom claim for admin

            if (user.Role == UserRole.Admin)
            {
                claims.Add(new Claim("CanRefundOrders", "true"));
            }

           

            var key = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: jwtSettings["Issuer"],
              audience: jwtSettings["Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
              signingCredentials: credentials
          );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
