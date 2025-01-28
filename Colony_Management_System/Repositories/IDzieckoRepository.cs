using Colony_Management_System.Models;
using System.Threading.Tasks;

namespace Colony_Management_System.Repositories
{
    public interface IDzieckoRepository
    {
        Task<List<Dziecko>> GetDzieciByRodzicIdAsync(int rodzicId);
        Task<Dziecko> GetDzieckoByIdAsync(int id);
        Task<Dziecko> AddDzieckoAsync(Dziecko dziecko);
        Task<Dziecko> UpdateDzieckoAsync(Dziecko dziecko);
        Task<bool> DeleteDzieckoAsync(int id);
    }
}
