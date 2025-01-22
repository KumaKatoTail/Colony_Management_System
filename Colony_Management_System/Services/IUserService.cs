using Microsoft.Identity.Client;

namespace Colony_Management_System.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> Authenticate(string email, string haslo);
    }

}
