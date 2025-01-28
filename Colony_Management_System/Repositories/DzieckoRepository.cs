using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Repositories
{
    public class DzieckoRepository : IDzieckoRepository
    {
        private readonly KoloniaDbContext _dbContext;

        public DzieckoRepository(KoloniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Dziecko>> GetDzieciByRodzicIdAsync(int rodzicId)
        {
            return await _dbContext.DzieckoRodzic
                .Where(dr => dr.RodzicId == rodzicId)
                .Select(dr => dr.Dziecko)
                .ToListAsync();
        }

        public async Task<Dziecko> GetDzieckoByIdAsync(int id)
        {
            return await _dbContext.Dziecko.FindAsync(id);
        }

        public async Task<Dziecko> AddDzieckoAsync(Dziecko dziecko)
        {
            _dbContext.Dziecko.Add(dziecko);
            await _dbContext.SaveChangesAsync();
            return dziecko;
        }

        public async Task<Dziecko> UpdateDzieckoAsync(Dziecko dziecko)
        {
            _dbContext.Dziecko.Update(dziecko);
            await _dbContext.SaveChangesAsync();
            return dziecko;
        }

        public async Task<bool> DeleteDzieckoAsync(int id)
        {
            var dziecko = await _dbContext.Dziecko.FindAsync(id);
            if (dziecko == null) return false;

            _dbContext.Dziecko.Remove(dziecko);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
