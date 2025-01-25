using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IKontoRepository
    {
        Task<Konto?> GetKontoByEmailAndPasswordAsync(string email, string haslo);
        Task<Administrator?> GetAdministratorByKontoIdAsync(int kontoId);
        Task<Opiekun?> GetOpiekunByKontoIdAsync(int kontoId);
        Task<Rodzic?> GetRodzicByKontoIdAsync(int kontoId);
    }
}
