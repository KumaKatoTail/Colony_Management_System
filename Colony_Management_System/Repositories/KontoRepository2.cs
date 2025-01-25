using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Repositories
{
    public class KontoRepository2 : IKontoRepository2
    {
        private readonly KoloniaDbContext _dbContext;

        public KontoRepository2(KoloniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Pobiera konto na podstawie adresu email
        public async Task<Konto?> GetKontoByEmailAsync(string email)
        {
            return await _dbContext.Konto
                .FirstOrDefaultAsync(k => k.Email == email);
        }

        // Pobiera dane administratora na podstawie kontoId
        public async Task<Administrator?> GetAdministratorByKontoIdAsync(int kontoId)
        {
            return await _dbContext.Administrator
                .Include(a => a.Firma) // Pobiera powiązaną firmę
                .FirstOrDefaultAsync(a => a.KontoId == kontoId);
        }

        // Pobiera dane opiekuna na podstawie kontoId
        public async Task<Opiekun?> GetOpiekunByKontoIdAsync(int kontoId)
        {
            return await _dbContext.Opiekun
                .Include(o => o.Konto) // Pobiera powiązane konto
                .FirstOrDefaultAsync(o => o.KontoId == kontoId);
        }

        // Pobiera dane rodzica na podstawie kontoId
        public async Task<Rodzic?> GetRodzicByKontoIdAsync(int kontoId)
        {
            return await _dbContext.Rodzic
                .Include(r => r.Adres) // Pobiera powiązany adres
                .Include(r => r.Konto) // Pobiera powiązane konto
                .FirstOrDefaultAsync(r => r.KontoId == kontoId);
        }
    }
}
