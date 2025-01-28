using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;
using Colony_Management_System.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateAdministrator([FromBody] AdministratorCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var createdAdministrator = await _administratorService.CreateAdministratorAsync(dto);
            return Ok(createdAdministrator);
        }

        // Read - Get by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdministratorById(int id)
        {
            var administrator = await _administratorService.GetByIdAsync(id);
            if (administrator == null)
                return NotFound("Administrator not found.");

            return Ok(administrator);
        }

        // Read - Get All
        [HttpGet]
        public async Task<IActionResult> GetAllAdministrators()
        {
            var administrators = await _administratorService.GetAllAsync();
            return Ok(administrators);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdministrator(int id, [FromBody] AdministratorUpdateDTO dto)
        {
            if (dto == null || id != dto.Id)
                return BadRequest("Invalid data.");

            var administrator = await _administratorService.GetByIdAsync(id);
            if (administrator == null)
                return NotFound("Administrator not found.");

            await _administratorService.UpdateAdministratorAsync(id, dto);
            return Ok("Administrator updated successfully.");
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrator(int id)
        {
            var administrator = await _administratorService.GetByIdAsync(id);
            if (administrator == null)
                return NotFound("Administrator not found.");

            await _administratorService.DeleteAdministratorAsync(id);
            return Ok("Administrator deleted successfully.");
        }
    }
}