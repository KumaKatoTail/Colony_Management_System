using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IKoloniaDzieckoRepository
    {
        Task<KoloniaDziecko> GetByIdAsync(int id);
        Task<List<KoloniaDziecko>> GetAllAsync();
        Task CreateAsync(KoloniaDzieckoCreateDTO dto);
        Task UpdateAsync(KoloniaDziecko koloniaDziecko);
        Task DeleteAsync(int id);
    }
}
