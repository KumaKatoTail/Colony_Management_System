using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Services
{
    public interface IGrupaService
    {
        Task<List<Grupa>> GetGrupyByKoloniaIdAsync(int koloniaId);
        Task<IEnumerable<GrupaDto>> GetGrupyByKoloniaIdZSAsync(int koloniaId);
    }
}
