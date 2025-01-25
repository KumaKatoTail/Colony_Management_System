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

    }
}
