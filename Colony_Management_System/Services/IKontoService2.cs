using System.Threading.Tasks;

namespace Colony_Management_System.Services
{
    public interface IKontoService2
    {
        Task<object?> GetZalogowaneKontoDetailsByEmailAsync(string email);
    }
}
