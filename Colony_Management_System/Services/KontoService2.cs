using System.Threading.Tasks;
using Colony_Management_System.Models;
using Colony_Management_System.Repositories;

namespace Colony_Management_System.Services
{
    public class KontoService2 : IKontoService2
    {
        private readonly IKontoRepository2 _kontoRepository;

        public KontoService2(IKontoRepository2 kontoRepository)
        {
            _kontoRepository = kontoRepository;
        }

        public async Task<object?> GetZalogowaneKontoDetailsByEmailAsync(string email)
        {
            var konto = await _kontoRepository.GetKontoByEmailAsync(email);

            if (konto == null)
                return null;

            switch (konto.UprId)
            {
                case 1: // Administrator
                    return await _kontoRepository.GetAdministratorByKontoIdAsync(konto.Id);
                case 2: // Opiekun
                    return await _kontoRepository.GetOpiekunByKontoIdAsync(konto.Id);
                case 3: // Rodzic
                    return await _kontoRepository.GetRodzicByKontoIdAsync(konto.Id);
                default:
                    return konto;
            }
        }
    }
}
