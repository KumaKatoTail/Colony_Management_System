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
    }
}
