using Colony_Management_System.Models;

namespace Colony_Management_System.Repositories
{
    public interface IAccountRepository
    {
        Task<Konto> GetAccountByEmailAsync(string email);
    }
}
