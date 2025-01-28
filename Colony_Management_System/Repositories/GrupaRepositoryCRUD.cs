using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Repositories
{
    public class GrupaRepositoryCRUD : IGrupaRepositoryCRUD
    {
        private readonly KoloniaDbContext _context;

        public GrupaRepositoryCRUD(KoloniaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Grupa>> GetAllAsync()
        {
            return await _context.Grupa.ToListAsync();
        }

        public async Task<Grupa> GetByIdAsync(int id)
        {
            return await _context.Grupa.FindAsync(id);
        }

        public async Task<Grupa> CreateAsync(Grupa newGrupa)
        {
            _context.Grupa.Add(newGrupa);
            await _context.SaveChangesAsync();
            return newGrupa;
        }

        public async Task<Grupa> UpdateAsync(int id, Grupa updatedGrupa)
        {
            var grupa = await _context.Grupa.FindAsync(id);

            if (grupa == null)
                return null;

            grupa.Temat = updatedGrupa.Temat;
            grupa.Opis = updatedGrupa.Opis;
            grupa.Limit = updatedGrupa.Limit;
            grupa.KoloniaId = updatedGrupa.KoloniaId;

            await _context.SaveChangesAsync();
            return grupa;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var grupa = await _context.Grupa.FindAsync(id);

            if (grupa == null)
                return false;

            _context.Grupa.Remove(grupa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
