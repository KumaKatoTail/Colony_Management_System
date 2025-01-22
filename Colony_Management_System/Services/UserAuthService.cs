namespace Colony_Management_System.Services
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Colony_Management_System.Models;
    using Colony_Management_System.Repositories;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System.Security.Cryptography;
    using Colony_Management_System.Models.Colony_Management_System.Models;

    public class UserAuthService : IUserAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly string _secretKey = "xLvOEFU2wtLi5tirZm7e6kGWC8txB3RhHF6JmeUJiBQ="; // Klucz do podpisywania tokenów

        public UserAuthService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(email);

            if (account == null || !VerifyPassword(account.Haslo, password)) // Sprawdzenie hasła
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return GenerateJwtToken(account);
        }

        private string GenerateJwtToken(Konto account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, GetRoleByUprId(account.UprId)) // Przypisanie roli na podstawie UprId
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ColonyManagementSystem",
                audience: "ColonyManagementSystemAPI",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetRoleByUprId(int uprId)
        {
            return uprId switch
            {
                1 => "Administrator", // Zakładamy, że 1 to Administrator
                2 => "Rodzic",       // 2 to Rodzic
                3 => "Opiekun",      // 3 to Opiekun
                _ => "User"          // Inne wartości to User
            };
        }

        // Funkcja do weryfikacji hasła
        private bool VerifyPassword(string storedHashedPassword, string inputPassword)
        {
            // Załóżmy, że hasło w bazie jest przechowywane jako "salt + hash"
            var salt = Convert.FromBase64String(storedHashedPassword.Substring(0, 24)); // Pierwsze 24 bajty to sól
            var storedHash = Convert.FromBase64String(storedHashedPassword.Substring(24));

            // Sprawdzenie, czy podane hasło pasuje do zapisanego hasła
            var computedHash = KeyDerivation.Pbkdf2(
                password: inputPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            return computedHash.SequenceEqual(storedHash);
        }

        // Funkcja do haszowania hasła przed zapisaniem
        public static string HashPassword(string password)
        {
            // Tworzenie soli
            var salt = new byte[24];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Haszowanie hasła
            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            // Zwracanie soli + hasz
            return Convert.ToBase64String(salt) + Convert.ToBase64String(hash);
        }
    }
}
