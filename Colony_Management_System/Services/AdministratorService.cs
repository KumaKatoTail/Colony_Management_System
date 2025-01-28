using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AdministratorService : IAdministratorService
{
    private readonly KoloniaDbContext _context;

    public AdministratorService(KoloniaDbContext context)
    {
        _context = context;
    }

    public async Task<Administrator> CreateAdministratorAsync(AdministratorCreateDTO dto)
    {
        var administrator = new Administrator
        {
            KontoId = dto.KontoId,
            FirmaId = dto.FirmaId
        };

        _context.Administrator.Add(administrator);
        await _context.SaveChangesAsync();

        return administrator;
    }

    public async Task<Administrator> GetByIdAsync(int id)
    {
        return await _context.Administrator
            .Include(a => a.Konto)
            .Include(a => a.Firma)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Administrator>> GetAllAsync()
    {
        return await _context.Administrator
            .Include(a => a.Konto)
            .Include(a => a.Firma)
            .ToListAsync();
    }

    public async Task UpdateAdministratorAsync(int id, AdministratorUpdateDTO dto)
    {
        var administrator = await _context.Administrator.FindAsync(id);
        if (administrator == null)
            throw new KeyNotFoundException("Administrator not found.");

        administrator.KontoId = dto.KontoId;
        administrator.FirmaId = dto.FirmaId;

        _context.Administrator.Update(administrator);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAdministratorAsync(int id)
    {
        var administrator = await _context.Administrator.FindAsync(id);
        if (administrator == null)
            throw new KeyNotFoundException("Administrator not found.");

        _context.Administrator.Remove(administrator);
        await _context.SaveChangesAsync();
    }
}
