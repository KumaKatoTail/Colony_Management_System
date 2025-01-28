using Colony_Management_System.Models.DbContext;

public class ZapisService
{
    private readonly KoloniaDbContext _context;

    public ZapisService(KoloniaDbContext context)
    {
        _context = context;
    }

    public int GetZapisaneDzieciByGrupaId(int grupaId)
    {
        // Załóżmy, że status 1 oznacza "Zapisane"
        int statusZapisany = 1;

        // Zliczamy liczbę zapisanych dzieci w danej grupie
        return _context.KoloniaDziecko
            .Count(z => z.GrupaId == grupaId && z.StatusId == statusZapisany);
    }
}
