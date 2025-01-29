using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colony_Management_System.Services
{
    public class PlatnoscService
    {
        private readonly KoloniaDbContext _context;

        public PlatnoscService(KoloniaDbContext context)
        {
            _context = context;
        }

        // Pobranie listy płatności po idRodzica i statusie
        public async Task<List<Platnosc>> GetPlatnosciByRodzicIdAsync(int rodzicId)
        {
            return await _context.Platnosc
                .Include(p => p.StatusPlatnosci) // Włączamy status płatności
                .Where(p => p.RodzicId == rodzicId)
                .ToListAsync();
        }

        // Pobranie pojedynczej płatności po id
        public async Task<Platnosc> GetPlatnoscByIdAsync(int id)
        {
            return await _context.Platnosc
                .Include(p => p.StatusPlatnosci) // Włączamy status płatności
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task UpdatePlatnoscStatusAsync(int platnoscId, int newStatusId)
        {
            var platnosc = await _context.Platnosc
                .FirstOrDefaultAsync(p => p.Id == platnoscId);

            if (platnosc != null)
            {
                platnosc.StatusId = newStatusId; // Przypisujemy nowy status
                _context.Platnosc.Update(platnosc); // Aktualizujemy płatność
                await _context.SaveChangesAsync(); // Zapisujemy zmiany w bazie
            }
        }
        public async Task<bool> RodzajPlatnosciExistsAsync(int rodzajPlatnosciId)
        {
            return await _context.RodzajPlatnosci.AnyAsync(rp => rp.Id == rodzajPlatnosciId);
        }
        public async Task<bool> WalutaExistsAsync(int walutaId)
        {
            return await _context.Waluta.AnyAsync(w => w.Id == walutaId);
        }
        public async Task<bool> KoloniaDzieckoExistsAsync(int koloniaDieckoId)
        {
            return await _context.KoloniaDziecko.AnyAsync(k => k.Id == koloniaDieckoId);
        }

        public async Task<Platnosc> GetPlatnoscByPaymentIdAsync(string paymentId)
        {
            return await _context.Platnosc
                .Include(p => p.StatusPlatnosci)
                .FirstOrDefaultAsync(p => p.NumerRef == paymentId);
        }
        public async Task<Platnosc> AddPlatnoscAsync(Platnosc platnosc)
        {
            _context.Platnosc.Add(platnosc); // Dodajemy nową płatność do DbContext
            await _context.SaveChangesAsync(); // Zapisujemy zmiany w bazie danych
            return platnosc; // Zwracamy dodany obiekt
        }



    }
}
