using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Services
{
    public interface IKoloniaDzieckoService
    {
        Task<KoloniaDziecko> GetByIdAsync(int id);
        Task<List<KoloniaDziecko>> GetAllAsync();
        Task CreateKoloniaDzieckoAsync(KoloniaDzieckoCreateDTO dto);
        Task UpdateKoloniaDzieckoAsync(KoloniaDziecko koloniaDziecko);
        Task DeleteKoloniaDzieckoAsync(int id);
        Task<List<KoloniaDzieckoRodzicDTO>> GetKoloniedzieciByRodzicIdAsync(int rodzicId); // Add this method
    }
}
