using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;

namespace Colony_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly KoloniaDbContext _context;
        private readonly string _secretKey = "supersecretkey123";  // W rzeczywistości przechowuj to w bezpieczniejszym miejscu

        public UserService(KoloniaDbContext context)
        {
            _context = context;
        }

        // Metoda do logowania użytkownika
        public async Task<AuthenticationResult> Authenticate(string email, string haslo)
        {
            // Wyszukiwanie użytkownika w bazie danych
            var user = await _context.Konto
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Haslo != haslo)  // Porównanie hasła (w rzeczywistości hasło powinno być zaszyfrowane)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password"
                };
            }

            // Generowanie tokenu JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenString
            };
        }
    }

    // Klasa wyników autentykacji
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
