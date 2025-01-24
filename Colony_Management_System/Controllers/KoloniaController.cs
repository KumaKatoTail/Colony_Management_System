﻿using Colony_Management_System.Models;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoloniaController : ControllerBase
    {
        private readonly IKoloniaService _koloniaService;

        public KoloniaController(IKoloniaService koloniaService)
        {
            _koloniaService = koloniaService;
        }

        // Dodawanie nowej kolonii
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddKolonia([FromBody] Kolonia kolonia)
        {
            if (kolonia == null)
                return BadRequest("Kolonia data is required.");

            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                var result = await _koloniaService.AddKoloniaAsync(kolonia, uprId.Value);
                return CreatedAtAction(nameof(GetKolonia), new { id = result.Id }, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can add colonies.");
            }
        }

        // Aktualizacja istniejącej kolonii
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateKolonia(int id, [FromBody] Kolonia kolonia)
        {
            if (kolonia == null)
                return BadRequest("Kolonia data is required.");

            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                var result = await _koloniaService.UpdateKoloniaAsync(id, kolonia, uprId.Value);
                if (result == null)
                    return NotFound("Kolonia not found.");
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can update colonies.");
            }
        }

        // Usuwanie kolonii
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteKolonia(int id)
        {
            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                var success = await _koloniaService.DeleteKoloniaAsync(id, uprId.Value);
                if (!success)
                    return NotFound("Kolonia not found.");
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can delete colonies.");
            }
        }

        // Pobieranie szczegółów kolonii
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetKolonia(int id)
        {
            var kolonia = await _koloniaService.GetKoloniaByIdAsync(id);
            if (kolonia == null)
                return NotFound("Kolonia not found.");
            return Ok(kolonia);
        }

        // Helper method to get UprId from token
        private int? GetUprIdFromToken()
        {
            var uprIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(uprIdClaim, out var uprId))
                return uprId;
            return null;
        }
    }
}
