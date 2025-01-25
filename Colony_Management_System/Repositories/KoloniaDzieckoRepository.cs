using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;

namespace Colony_Management_System.Repositories
{
    public class KoloniaDzieckoRepository : IKoloniaDzieckoRepository
    {
        private readonly KoloniaDbContext _dbContext;

        public KoloniaDzieckoRepository(KoloniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<KoloniaDziecko>> GetAllAsync()
        {
            return await _dbContext.KoloniaDziecko
                .Include(kd => kd.Dziecko)
                .Include(kd => kd.Grupa)
                .Include(kd => kd.Status)
                .ToListAsync();
        }

        public async Task<KoloniaDziecko> GetByIdAsync(int id)
        {
            return await _dbContext.KoloniaDziecko
                .Include(kd => kd.Dziecko)
                .Include(kd => kd.Grupa)
                .Include(kd => kd.Status)
                .FirstOrDefaultAsync(kd => kd.Id == id);
        }

        public async Task CreateAsync(KoloniaDzieckoCreateDTO dto)
        {
            var koloniaDziecko = new KoloniaDziecko
            {
                DzieckoId = dto.DzieckoId,
                GrupaId = dto.GrupaId,
                StatusId = dto.StatusId,
            };

            await _dbContext.KoloniaDziecko.AddAsync(koloniaDziecko);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(KoloniaDziecko koloniaDziecko)
        {
            _dbContext.KoloniaDziecko.Update(koloniaDziecko);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var koloniaDziecko = await GetByIdAsync(id);
            if (koloniaDziecko != null)
            {
                _dbContext.KoloniaDziecko.Remove(koloniaDziecko);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
