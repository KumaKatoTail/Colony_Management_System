namespace Colony_Management_System.Services
{
    using Colony_Management_System.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdministratorService
    {
        Task<Administrator> CreateAdministratorAsync(AdministratorCreateDTO dto);
        Task<Administrator> GetByIdAsync(int id);
        Task<IEnumerable<Administrator>> GetAllAsync();
        Task UpdateAdministratorAsync(int id, AdministratorUpdateDTO dto);
        Task DeleteAdministratorAsync(int id);
    }

}
