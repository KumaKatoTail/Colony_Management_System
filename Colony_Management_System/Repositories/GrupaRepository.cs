using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;

namespace Colony_Management_System.Repositories
{
    public class GrupaRepository : IGrupaRepository
    {
        private readonly KoloniaDbContext _dbContext;

        public GrupaRepository(KoloniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Grupa>> GetGrupyByKoloniaIdAsync(int koloniaId)
        {
            return await _dbContext.Grupa
                .Where(g => g.KoloniaId == koloniaId)
                .ToListAsync();
        }
    }
}
