using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IDzieckoRepository
    {
        Task<List<Dziecko>> GetDzieciByRodzicIdAsync(int rodzicId);
    }
}
