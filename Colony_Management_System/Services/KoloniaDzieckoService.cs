using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Colony_Management_System.Services
{
    public class KoloniaDzieckoService : IKoloniaDzieckoService
    {
        private readonly IKoloniaDzieckoRepository _koloniaDzieckoRepository;
        private readonly KoloniaDbContext _context;

        public KoloniaDzieckoService(IKoloniaDzieckoRepository koloniaDzieckoRepository, KoloniaDbContext context)
        {
            _koloniaDzieckoRepository = koloniaDzieckoRepository;
            _context = context;
        }

        public async Task<KoloniaDziecko> GetByIdAsync(int id)
        {
            return await _koloniaDzieckoRepository.GetByIdAsync(id);
        }

        public async Task<List<KoloniaDziecko>> GetAllAsync()
        {
            return await _koloniaDzieckoRepository.GetAllAsync();
        }

        public async Task CreateKoloniaDzieckoAsync(KoloniaDzieckoCreateDTO dto)
        {
            await _koloniaDzieckoRepository.CreateAsync(dto);
        }

        public async Task UpdateKoloniaDzieckoAsync(KoloniaDziecko koloniaDziecko)
        {
            await _koloniaDzieckoRepository.UpdateAsync(koloniaDziecko);
        }

        public async Task DeleteKoloniaDzieckoAsync(int id)
        {
            await _koloniaDzieckoRepository.DeleteAsync(id);
        }
        public async Task<List<KoloniaDzieckoRodzicDTO>> GetKoloniedzieciByRodzicIdAsync(int rodzicId)
        {
            var result = await _context.DzieckoRodzic
                .Where(dr => dr.RodzicId == rodzicId)
                .Join(_context.Dziecko, dr => dr.DzieckoId, d => d.Id, (dr, d) => new { dr, d })
                .Join(_context.KoloniaDziecko, dd => dd.d.Id, kd => kd.DzieckoId, (dd, kd) => new { dd, kd })
                .Join(_context.Grupa, kd => kd.kd.GrupaId, g => g.Id, (kd, g) => new { kd, g })
                .Join(_context.Kolonia, g => g.g.KoloniaId, k => k.Id, (g, k) => new KoloniaDzieckoRodzicDTO
                {
                    NazwaKolonii = k.Nazwa,
                    IdKolonii = k.Id,
                    NazwaGrupy = g.g.Temat, // Assuming 'Temat' is the group name
                    IdGrupy = g.g.Id,
                    OpisGrupy = g.g.Opis,
                    ImieDziecka = g.kd.kd.Dziecko.Imie,
                    IdDziecka = g.kd.kd.Dziecko.Id,
                    Status = g.kd.kd.Status.Nazwa // Assuming Status is an object with a 'Nazwa' property
                })
                .ToListAsync();

            return result;
        }



    }
}
