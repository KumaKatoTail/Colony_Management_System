using System.Threading.Tasks;
using Colony_Management_System.Models;

namespace Colony_Management_System.Services
{
    public interface IKontoService
    {
        Task<object?> GetZalogowaneKontoDetailsAsync(string email, string haslo);
        Task<Konto?> GetKontoByEmailAsync(string email);
    }
}
