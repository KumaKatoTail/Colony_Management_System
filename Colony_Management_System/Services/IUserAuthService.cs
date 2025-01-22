namespace Colony_Management_System.Services
{
    public interface IUserAuthService
    {
        Task<string> LoginAsync(string email, string password);
    }

}
