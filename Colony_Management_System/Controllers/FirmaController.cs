using Colony_Management_System.Models;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmaController : ControllerBase
    {
        private readonly IFirmaService _firmaService;

        public FirmaController(IFirmaService firmaService)
        {
            _firmaService = firmaService;
        }

        // GET: api/firma/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFirmaById(int id)
        {
            var firma = await _firmaService.GetByIdAsync(id);
            if (firma == null)
                return NotFound("Firma not found.");

            return Ok(firma);
        }

        // GET: api/firma
        [HttpGet]
        public async Task<IActionResult> GetAllFirma()
        {
            var firmy = await _firmaService.GetAllAsync();
            return Ok(firmy);
        }
    }
}

