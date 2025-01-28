using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IGrupaRepositoryCRUD
    {
        Task<List<Grupa>> GetAllAsync();
        Task<Grupa> GetByIdAsync(int id);
        Task<Grupa> CreateAsync(Grupa newGrupa);
        Task<Grupa> UpdateAsync(int id, Grupa updatedGrupa);
        Task<bool> DeleteAsync(int id);
    }
}
