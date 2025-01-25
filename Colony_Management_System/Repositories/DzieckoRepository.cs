using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;

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
    }
}
