using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Services
{
    public class KontoService : IKontoService
    {
        private readonly IKontoRepository _kontoRepository;
        private readonly KoloniaDbContext _context;

        public KontoService(IKontoRepository kontoRepository, KoloniaDbContext context)
        {
            _kontoRepository = kontoRepository;
            _context = context;
        }
      
        public async Task<object?> GetZalogowaneKontoDetailsAsync(string email, string haslo)
        {
            var konto = await _kontoRepository.GetKontoByEmailAndPasswordAsync(email, haslo);

            if (konto == null)
                return null;

            switch (konto.UprId)
            {
                case 1: // Administrator
                    return await _kontoRepository.GetAdministratorByKontoIdAsync(konto.Id);
                case 2: // Opiekun
                    return await _kontoRepository.GetOpiekunByKontoIdAsync(konto.Id);
                case 3: // Rodzic
                    return await _kontoRepository.GetRodzicByKontoIdAsync(konto.Id);
                default:
                    return konto;
            }
        }
        public async Task<Konto?> GetKontoByEmailAsync(string email)
        {
            return await _context.Konto
                .Include(k => k.Upr) // Załaduj powiązane dane z tabeli Upr
                .FirstOrDefaultAsync(k => k.Email == email);
        }
    }
}
