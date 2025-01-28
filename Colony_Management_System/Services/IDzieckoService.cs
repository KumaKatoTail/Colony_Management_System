using Colony_Management_System.Models;

namespace Colony_Management_System.Services
{
    public interface IDzieckoService
    {
        Task<List<Dziecko>> GetDzieciByRodzicIdAsync(int rodzicId);
        Task<Dziecko> GetDzieckoByIdAsync(int id);
        Task<Dziecko> AddDzieckoAsync(Dziecko dziecko);
        Task<Dziecko> UpdateDzieckoAsync(Dziecko dziecko);
        Task<bool> DeleteDzieckoAsync(int id);
    }
}
