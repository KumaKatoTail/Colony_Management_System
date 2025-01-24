﻿using Colony_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Colony_Management_System.Models.DbContext;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly KoloniaDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(KoloniaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Endpoint do logowania użytkownika
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] KontoLoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Haslo))
            {
                return BadRequest("Email and password are required.");
            }

            // Wyszukiwanie użytkownika w bazie danych
            var konto = await _context.Konto.FirstOrDefaultAsync(k => k.Email == model.Email);

            if (konto == null || konto.Haslo != model.Haslo) // Bez haszowania hasła
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generowanie tokenu JWT
            var token = GenerateJwtToken(konto);

            return Ok(new { Token = token });
        }

        // Endpoint do weryfikacji tokenu
        [HttpPost("CheckToken")]
        [Authorize]
        public IActionResult CheckToken()
        {
            return Ok(new { Message = "Token is valid." });
        }

        // Generowanie tokenu JWT
        private string GenerateJwtToken(Konto konto)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, konto.Email),
                new Claim(ClaimTypes.Role, konto.UprId.ToString()), // Przypisanie roli użytkownika
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    // ViewModel do logowania
    public class KontoLoginViewModel
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
    }
}
