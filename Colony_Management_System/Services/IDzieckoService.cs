using Colony_Management_System.Models;

namespace Colony_Management_System.Services
{
    public interface IDzieckoService
    {
        Task<List<Dziecko>> GetDzieciByRodzicIdAsync(int rodzicId);
    }
}
