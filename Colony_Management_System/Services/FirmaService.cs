namespace Colony_Management_System.Services
{
    using Colony_Management_System.Models;
    using Colony_Management_System.Models.DbContext;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FirmaService : IFirmaService
    {
        private readonly KoloniaDbContext _context;

        public FirmaService(KoloniaDbContext context)
        {
            _context = context;
        }

        public async Task<Firma> CreateFirmaAsync(FirmaCreateDTO dto)
        {
            var firma = new Firma
            {
                AdresId = dto.AdresId,
                Nazwa = dto.Nazwa
            };

            _context.Firma.Add(firma);
            await _context.SaveChangesAsync();

            return firma;
        }

        public async Task<Firma> GetByIdAsync(int id)
        {
            return await _context.Firma
                .Include(f => f.Adres)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Firma>> GetAllAsync()
        {
            return await _context.Firma
                .Include(f => f.Adres)
                .ToListAsync();
        }

        public async Task UpdateFirmaAsync(int id, FirmaUpdateDTO dto)
        {
            var firma = await _context.Firma.FindAsync(id);
            if (firma == null)
                throw new KeyNotFoundException("Firma not found.");

            firma.AdresId = dto.AdresId;
            firma.Nazwa = dto.Nazwa;

            _context.Firma.Update(firma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFirmaAsync(int id)
        {
            var firma = await _context.Firma.FindAsync(id);
            if (firma == null)
                throw new KeyNotFoundException("Firma not found.");

            _context.Firma.Remove(firma);
            await _context.SaveChangesAsync();
        }
    }

}
