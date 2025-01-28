using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colony_Management_System.Services
{
    public class RodzicService
    {
        private readonly KoloniaDbContext _context;

        public RodzicService(KoloniaDbContext context)
        {
            _context = context;
        }

        // Get list of rodzice
        public async Task<List<Rodzic>> GetRodziceListAsync()
        {
            return await _context.Rodzic
                .Include(r => r.Konto)
                .Include(r => r.Adres)
                .ToListAsync();
        }

        // Get rodzic by id
        public async Task<Rodzic> GetRodzicByIdAsync(int id)
        {
            return await _context.Rodzic
                .Include(r => r.Konto)
                .Include(r => r.Adres)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Create new rodzic
        public async Task<Rodzic> CreateRodzicAsync(Rodzic rodzic)
        {
            _context.Rodzic.Add(rodzic);
            await _context.SaveChangesAsync();
            return rodzic;
        }

        // Update existing rodzic
        public async Task<Rodzic> UpdateRodzicAsync(int id, Rodzic rodzic)
        {
            var existingRodzic = await _context.Rodzic.FindAsync(id);
            if (existingRodzic == null) return null;

            existingRodzic.Imie = rodzic.Imie;
            existingRodzic.Nazwisko = rodzic.Nazwisko;
            existingRodzic.Telefon = rodzic.Telefon;
            existingRodzic.Mail = rodzic.Mail;
            existingRodzic.AdresId = rodzic.AdresId;

            _context.Rodzic.Update(existingRodzic);
            await _context.SaveChangesAsync();
            return existingRodzic;
        }

        // Delete rodzic
        public async Task<bool> DeleteRodzicAsync(int id)
        {
            var rodzic = await _context.Rodzic.FindAsync(id);
            if (rodzic == null) return false;

            _context.Rodzic.Remove(rodzic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

