namespace Colony_Management_System.Services
{
    using Colony_Management_System.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFirmaService
    {
        Task<Firma> CreateFirmaAsync(FirmaCreateDTO dto);
        Task<Firma> GetByIdAsync(int id);
        Task<IEnumerable<Firma>> GetAllAsync();
        Task UpdateFirmaAsync(int id, FirmaUpdateDTO dto);
        Task DeleteFirmaAsync(int id);
    }
}
