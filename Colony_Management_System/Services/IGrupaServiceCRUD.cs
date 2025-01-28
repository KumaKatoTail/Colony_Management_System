using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Services
{
    public interface IGrupaServiceCRUD
    {
        Task<List<Grupa>> GetAllGrupyAsync();
        Task<Grupa> GetGrupaByIdAsync(int id);
        Task<Grupa> CreateGrupaAsync(Grupa newGrupa);
        Task<Grupa> UpdateGrupaAsync(int id, Grupa updatedGrupa);
        Task<bool> DeleteGrupaAsync(int id);
    }
}
