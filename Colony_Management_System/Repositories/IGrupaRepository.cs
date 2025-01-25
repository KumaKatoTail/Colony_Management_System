using System.Collections.Generic;
using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IGrupaRepository
    {
        Task<List<Grupa>> GetGrupyByKoloniaIdAsync(int koloniaId);
    }
}
