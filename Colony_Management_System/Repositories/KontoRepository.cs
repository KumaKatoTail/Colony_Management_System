using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Repositories
{
    public class KontoRepository : IKontoRepository
    {
        private readonly KoloniaDbContext _dbContext;

        public KontoRepository(KoloniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Konto?> GetKontoByEmailAndPasswordAsync(string email, string haslo)
        {
            return await _dbContext.Konto
                .FirstOrDefaultAsync(k => k.Email == email && k.Haslo == haslo);
        }

        public async Task<Administrator?> GetAdministratorByKontoIdAsync(int kontoId)
        {
            return await _dbContext.Administrator
                .Include(a => a.Firma)
                .FirstOrDefaultAsync(a => a.KontoId == kontoId);
        }

        public async Task<Opiekun?> GetOpiekunByKontoIdAsync(int kontoId)
        {
            return await _dbContext.Opiekun
                .FirstOrDefaultAsync(o => o.KontoId == kontoId);
        }

        public async Task<Rodzic?> GetRodzicByKontoIdAsync(int kontoId)
        {
            return await _dbContext.Rodzic
                .Include(r => r.Adres)
                .FirstOrDefaultAsync(r => r.KontoId == kontoId);
        }
    }
}
