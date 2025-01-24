using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;
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

        // Pobieranie konta na podstawie adresu e-mail
        public async Task<Konto> GetAccountByEmailAsync(string email)
        {
            return await _context.Konto
                .FirstOrDefaultAsync(k => k.Email == email);
        }

        // Weryfikacja email i hasła
        public async Task<bool> VerifyEmailAndPasswordAsync(string email, string haslo)
        {
            // Pobierz konto na podstawie e-maila
            var konto = await _context.Konto
                .FirstOrDefaultAsync(k => k.Email == email);

            // Jeśli konto nie istnieje lub hasło się nie zgadza, zwróć false
            if (konto == null || konto.Haslo != haslo)
            {
                return false;
            }

            // Jeśli e-mail i hasło są poprawne, zwróć true
            return true;
        }
    }
}
