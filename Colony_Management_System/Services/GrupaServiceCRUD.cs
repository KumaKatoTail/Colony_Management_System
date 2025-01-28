using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Repositories;

namespace Colony_Management_System.Services
{
    public class GrupaServiceCRUD : IGrupaServiceCRUD
    {
        private readonly IGrupaRepositoryCRUD _grupaRepository;

        public GrupaServiceCRUD(IGrupaRepositoryCRUD grupaRepository)
        {
            _grupaRepository = grupaRepository;
        }

        public async Task<List<Grupa>> GetAllGrupyAsync()
        {
            return await _grupaRepository.GetAllAsync();
        }

        public async Task<Grupa> GetGrupaByIdAsync(int id)
        {
            return await _grupaRepository.GetByIdAsync(id);
        }

        public async Task<Grupa> CreateGrupaAsync(Grupa newGrupa)
        {
            return await _grupaRepository.CreateAsync(newGrupa);
        }

        public async Task<Grupa> UpdateGrupaAsync(int id, Grupa updatedGrupa)
        {
            return await _grupaRepository.UpdateAsync(id, updatedGrupa);
        }

        public async Task<bool> DeleteGrupaAsync(int id)
        {
            return await _grupaRepository.DeleteAsync(id);
        }
    }
}
