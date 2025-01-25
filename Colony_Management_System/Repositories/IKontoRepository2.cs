using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IKontoRepository2
    {
        Task<Konto?> GetKontoByEmailAsync(string email);
        Task<Administrator?> GetAdministratorByKontoIdAsync(int kontoId);
        Task<Opiekun?> GetOpiekunByKontoIdAsync(int kontoId);
        Task<Rodzic?> GetRodzicByKontoIdAsync(int kontoId);
    }
}

