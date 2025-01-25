using Colony_Management_System.Models;
using Colony_Management_System.Repositories;
using System.Threading.Tasks;

namespace Colony_Management_System.Services
{
    public interface IKoloniaService
    {
        Task<Kolonia> GetKoloniaByIdAsync(int id);
        Task<Kolonia> AddKoloniaAsync(Kolonia kolonia, int uprId);
        Task<Kolonia> UpdateKoloniaAsync(int id, Kolonia kolonia, int uprId);
        Task<bool> DeleteKoloniaAsync(int id, int uprId);
        Task<IEnumerable<Kolonia>> GetAllKolonieAsync();
    }

    public class KoloniaService : IKoloniaService
    {
        private readonly IKoloniaRepository _koloniaRepository;

        public KoloniaService(IKoloniaRepository koloniaRepository)
        {
            _koloniaRepository = koloniaRepository;
        }

        public async Task<Kolonia> GetKoloniaByIdAsync(int id)
        {
            return await _koloniaRepository.GetKoloniaByIdAsync(id);
        }

        public async Task<Kolonia> AddKoloniaAsync(Kolonia kolonia, int uprId)
        {
            if (uprId != 1) // Sprawdzenie, czy użytkownik jest administratorem
                throw new UnauthorizedAccessException("Only administrators can add colonies.");

            return await _koloniaRepository.AddKoloniaAsync(kolonia);
        }

        public async Task<Kolonia> UpdateKoloniaAsync(int id, Kolonia kolonia, int uprId)
        {
            if (uprId != 1) // Sprawdzenie, czy użytkownik jest administratorem
                throw new UnauthorizedAccessException("Only administrators can update colonies.");

            return await _koloniaRepository.UpdateKoloniaAsync(id, kolonia);
        }

        public async Task<bool> DeleteKoloniaAsync(int id, int uprId)
        {
            if (uprId != 1) // Sprawdzenie, czy użytkownik jest administratorem
                throw new UnauthorizedAccessException("Only administrators can delete colonies.");

            return await _koloniaRepository.DeleteKoloniaAsync(id);
        }
        public async Task<IEnumerable<Kolonia>> GetAllKolonieAsync()
        {
            return await _koloniaRepository.GetAllKolonieAsync();
        }

        
    }
}
