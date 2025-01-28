using Colony_Management_System.Models;
using Colony_Management_System.Repositories;

namespace Colony_Management_System.Services
{
    public class DzieckoService : IDzieckoService
    {
        private readonly IDzieckoRepository _dzieckoRepository;

        public DzieckoService(IDzieckoRepository dzieckoRepository)
        {
            _dzieckoRepository = dzieckoRepository;
        }

        public async Task<List<Dziecko>> GetDzieciByRodzicIdAsync(int rodzicId)
        {
            return await _dzieckoRepository.GetDzieciByRodzicIdAsync(rodzicId);
        }

        public async Task<Dziecko> GetDzieckoByIdAsync(int id)
        {
            return await _dzieckoRepository.GetDzieckoByIdAsync(id);
        }

        public async Task<Dziecko> AddDzieckoAsync(Dziecko dziecko)
        {
            return await _dzieckoRepository.AddDzieckoAsync(dziecko);
        }

        public async Task<Dziecko> UpdateDzieckoAsync(Dziecko dziecko)
        {
            return await _dzieckoRepository.UpdateDzieckoAsync(dziecko);
        }

        public async Task<bool> DeleteDzieckoAsync(int id)
        {
            return await _dzieckoRepository.DeleteDzieckoAsync(id);
        }
    }
}
