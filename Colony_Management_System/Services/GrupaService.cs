using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Services
{
    public class GrupaService : IGrupaService
    {
        private readonly IGrupaRepository _grupaRepository;
        private readonly KoloniaDbContext _context;

        public GrupaService(IGrupaRepository grupaRepository, KoloniaDbContext context)
        {
            _grupaRepository = grupaRepository;
            _context = context;
        }

        public async Task<List<Grupa>> GetGrupyByKoloniaIdAsync(int koloniaId)
        {
            return await _grupaRepository.GetGrupyByKoloniaIdAsync(koloniaId);
        }
        public async Task<IEnumerable<GrupaDto>> GetGrupyByKoloniaIdZSAsync(int koloniaId)
        {
            var grupy = await _context.Grupa
                .Where(g => g.KoloniaId == koloniaId)
                .ToListAsync();

            var grupyDto = grupy.Select(g => new GrupaDto
            {
                Id = g.Id,
                KoloniaId = g.KoloniaId,
                Temat = g.Temat,
                Opis = g.Opis,
                Limit = g.Limit,
                WolneMiejsca = g.Limit - _context.KoloniaDziecko.Count(kd => kd.GrupaId == g.Id)
            });

            return grupyDto;
        }

    }
}
