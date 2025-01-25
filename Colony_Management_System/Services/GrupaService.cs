using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Repositories;

namespace Colony_Management_System.Services
{
    public class GrupaService : IGrupaService
    {
        private readonly IGrupaRepository _grupaRepository;

        public GrupaService(IGrupaRepository grupaRepository)
        {
            _grupaRepository = grupaRepository;
        }

        public async Task<List<Grupa>> GetGrupyByKoloniaIdAsync(int koloniaId)
        {
            return await _grupaRepository.GetGrupyByKoloniaIdAsync(koloniaId);
        }
    }
}
