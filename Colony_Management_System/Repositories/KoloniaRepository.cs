using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Colony_Management_System.Repositories
{
    public interface IKoloniaRepository
    {
        Task<Kolonia> AddKoloniaAsync(Kolonia kolonia);
        Task<Kolonia> UpdateKoloniaAsync(int id, Kolonia kolonia);
        Task<bool> DeleteKoloniaAsync(int id);
        Task<Kolonia> GetKoloniaByIdAsync(int id);
    }

    public class KoloniaRepository : IKoloniaRepository
    {
        private readonly KoloniaDbContext _context;

        public KoloniaRepository(KoloniaDbContext context)
        {
            _context = context;
        }

        public async Task<Kolonia> GetKoloniaByIdAsync(int id)
        {
            return await _context.Kolonia
                .Include(k => k.Firma)
                .Include(k => k.Adres)
                .Include(k => k.Forma)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<Kolonia> AddKoloniaAsync(Kolonia kolonia)
        {
            _context.Kolonia.Add(kolonia);
            await _context.SaveChangesAsync();
            return kolonia;
        }

        public async Task<Kolonia> UpdateKoloniaAsync(int id, Kolonia kolonia)
        {
            var existingKolonia = await _context.Kolonia.FindAsync(id);
            if (existingKolonia == null)
                return null;

            existingKolonia.FirmaId = kolonia.FirmaId;
            existingKolonia.AdresId = kolonia.AdresId;
            existingKolonia.FormaId = kolonia.FormaId;
            existingKolonia.TerminOd = kolonia.TerminOd;
            existingKolonia.TerminDo = kolonia.TerminDo;
            existingKolonia.TrasaWedrowna = kolonia.TrasaWedrowna;
            existingKolonia.Kraj = kolonia.Kraj;

            await _context.SaveChangesAsync();
            return existingKolonia;
        }

        public async Task<bool> DeleteKoloniaAsync(int id)
        {
            var kolonia = await _context.Kolonia.FindAsync(id);
            if (kolonia == null)
                return false;

            _context.Kolonia.Remove(kolonia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
