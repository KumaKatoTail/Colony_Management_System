using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Colony_Management_System.Models.DbContext;

namespace Colony_Management_System.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly KoloniaDbContext _context;

        public AccountRepository(KoloniaDbContext context)
        {
            _context = context;
        }

        public async Task<Konto> GetAccountByEmailAsync(string email)
        {
            return await _context.Konto
                .FirstOrDefaultAsync(k => k.Email == email);
        }

        // Metoda do porównania hasła
        public bool VerifyPassword(Konto konto, string haslo)
        {
            // Zakładając, że hasło jest przechowywane jako hash w bazie danych
            var hashedPassword = konto.Haslo;

            // Sprawdzanie hasła przy użyciu tej samej metody haszowania
            return VerifyHashedPassword(hashedPassword, haslo);
        }

        Task<Konto> IAccountRepository.GetAccountByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }


        private bool VerifyHashedPassword(string hashedPassword, string haslo)
        {
            // Metoda do porównania hasła z zahashowaną wersją w bazie danych
            var salt = Convert.FromBase64String(hashedPassword.Substring(0, 24)); // Załóżmy, że salt jest zapisany w pierwszych 24 bajtach
            var storedHash = Convert.FromBase64String(hashedPassword.Substring(24));

            // Sprawdzenie, czy podane hasło po haszowaniu pasuje do zapisanego hasła
            var computedHash = KeyDerivation.Pbkdf2(
                password: haslo,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            return computedHash.SequenceEqual(storedHash);
        }
    }
}
