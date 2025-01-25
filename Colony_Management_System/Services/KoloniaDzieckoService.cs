using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Repositories;

namespace Colony_Management_System.Services
{
    public class KoloniaDzieckoService : IKoloniaDzieckoService
    {
        private readonly IKoloniaDzieckoRepository _koloniaDzieckoRepository;

        public KoloniaDzieckoService(IKoloniaDzieckoRepository koloniaDzieckoRepository)
        {
            _koloniaDzieckoRepository = koloniaDzieckoRepository;
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
    }
}
