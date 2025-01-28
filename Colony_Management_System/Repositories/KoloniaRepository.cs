using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Colony_Management_System.Repositories
{
    public interface IKoloniaRepository
    {
        Task<Kolonia?> AddKoloniaAsync(Kolonia kolonia);
        Task<Kolonia> UpdateKoloniaAsync(int id, Kolonia kolonia);

        Task<bool> DeleteKoloniaAsync(int id);
        Task<Kolonia?> GetKoloniaByIdAsync(int id);
        Task<IEnumerable<Kolonia>> GetAllKolonieAsync();
    }

    public class KoloniaRepository : IKoloniaRepository
    {
        private readonly KoloniaDbContext _context;

        public KoloniaRepository(KoloniaDbContext context)
        {
            _context = context;
        }

        public async Task<Kolonia?> GetKoloniaByIdAsync(int id)
        {
            try
            {
                return await _context.Kolonia
                    .Include(k => k.Firma)
                    .Include(k => k.Adres)
                    .Include(k => k.Forma)
                    .FirstOrDefaultAsync(k => k.Id == id);
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                return null;
            }
        }

        public async Task<Kolonia?> AddKoloniaAsync(Kolonia kolonia)
        {
            try
            {
                _context.Kolonia.Add(kolonia);
                await _context.SaveChangesAsync();
                return kolonia;
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                return null;
            }
        }

        public async Task<Kolonia> UpdateKoloniaAsync(int id, Kolonia kolonia)
        {
            var existingKolonia = await _context.Kolonia.FindAsync(id);
            if (existingKolonia == null)
                throw new KeyNotFoundException("Kolonia not found.");

            // Aktualizacja danych
            _context.Entry(existingKolonia).CurrentValues.SetValues(kolonia);

            // Zapis zmian w bazie
            await _context.SaveChangesAsync();
            return existingKolonia;
        }


        public async Task<bool> DeleteKoloniaAsync(int id)
        {
            try
            {
                var kolonia = await _context.Kolonia.FindAsync(id);
                if (kolonia == null)
                    return false;

                _context.Kolonia.Remove(kolonia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary
                return false;
            }
        }
        public async Task<IEnumerable<Kolonia>> GetAllKolonieAsync()
        {
            return await _context.Kolonia.ToListAsync();
        }

    }
}
