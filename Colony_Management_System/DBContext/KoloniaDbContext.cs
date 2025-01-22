using Colony_Management_System.Models.Colony_Management_System.Models;
using Colony_Management_System.Models;
using Microsoft.EntityFrameworkCore;

public class KoloniaDbContext : DbContext
{

    readonly string connectionString;


    public KoloniaDbContext()
    {
    }
    //------------------------------------------------------------------------------------------------

#pragma warning disable CS8618
    public KoloniaDbContext(DbContextOptions options)
         : base(options)
#pragma warning restore CS8618
    {
    }
    //------------------------------------------------------------------------------------------------

#pragma warning disable CS8618
    public KoloniaDbContext(string connectionString)
#pragma warning restore CS8618
    {
        this.connectionString = connectionString;
    }

    public DbSet<Administrator> Administrator { get; set; }
    public DbSet<Adres> Adres { get; set; }
    public DbSet<Dziecko> Dziecko { get; set; }
    public DbSet<DzieckoRodzic> DzieckoRodzic { get; set; }
    public DbSet<Firma> Firma { get; set; }
    public DbSet<Grupa> Grupa { get; set; }
    public DbSet<Kolonia> Kolonia { get; set; }
    public DbSet<KoloniaDziecko> KoloniaDziecko { get; set; }
    public DbSet<Konto> Konto { get; set; }
    public DbSet<Miasto> Miasto { get; set; }
    public DbSet<Obserwacja> Obserwacja { get; set; }
    public DbSet<Opiekun> Opiekun { get; set; }
    public DbSet<OpiekunGrupa> OpiekunGrupa { get; set; }
    public DbSet<Platnosc> Platnosc { get; set; }
    public DbSet<Rodzic> Rodzic { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Ulica> Ulica { get; set; }
    public DbSet<Forma> Forma { get; set; }
    public DbSet<RodzObs> RodzObs { get; set; }
    public DbSet<RodzajPlatnosci> RodzajPlatnosci { get; set; }
    public DbSet<StatusPlatnosci> StatusPlatnosci { get; set; }
    public DbSet<Upr> Upr { get; set; }
    public DbSet<Waluta> Waluta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureAdministrator(modelBuilder);
        ConfigureAdres(modelBuilder);
        ConfigureDziecko(modelBuilder);
        ConfigureDzieckoRodzic(modelBuilder);
        ConfigureFirma(modelBuilder);
        ConfigureGrupa(modelBuilder);
        ConfigureKolonia(modelBuilder);
        ConfigureKoloniaDziecko(modelBuilder);
        ConfigureKonto(modelBuilder);
        ConfigureMiasto(modelBuilder);
        ConfigureObserwacja(modelBuilder);
        ConfigureOpiekun(modelBuilder);
        ConfigureOpiekunGrupa(modelBuilder);
        ConfigurePlatnosc(modelBuilder);
        ConfigureRodzic(modelBuilder);
        ConfigureStatus(modelBuilder);
        ConfigureUlica(modelBuilder);
        ConfigureForma(modelBuilder);
        ConfigureRodzObs(modelBuilder);
        ConfigureRodzajPlatnosci(modelBuilder);
        ConfigureStatusPlatnosci(modelBuilder);
        ConfigureUpr(modelBuilder);
        ConfigureWaluta(modelBuilder);
    }

    private void ConfigureAdministrator(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<Administrator>()
            .HasOne(a => a.Firma)
            .WithMany()
            .HasForeignKey(a => a.FirmaId);
        modelBuilder.Entity<Administrator>()
            .HasOne(a => a.Konto)
            .WithMany()
            .HasForeignKey(a => a.KontoId);
    }

    private void ConfigureAdres(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adres>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<Adres>()
            .HasOne(a => a.Ulica)
            .WithMany()
            .HasForeignKey(a => a.UlicaId);
    }

    private void ConfigureDziecko(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dziecko>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Dziecko>()
            .HasOne(d => d.Adres)
            .WithMany()
            .HasForeignKey(d => d.AdresId);
    }

    private void ConfigureDzieckoRodzic(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DzieckoRodzic>()
            .HasKey(dr => dr.Id);
        modelBuilder.Entity<DzieckoRodzic>()
            .HasOne(dr => dr.Dziecko)
            .WithMany()
            .HasForeignKey(dr => dr.DzieckoId);
        modelBuilder.Entity<DzieckoRodzic>()
            .HasOne(dr => dr.Rodzic)
            .WithMany()
            .HasForeignKey(dr => dr.RodzicId);
    }

    private void ConfigureFirma(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Firma>()
            .HasKey(f => f.Id);
        modelBuilder.Entity<Firma>()
            .HasOne(f => f.Adres)
            .WithMany()
            .HasForeignKey(f => f.AdresId);
    }

    private void ConfigureGrupa(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grupa>()
            .HasKey(g => g.Id);
        modelBuilder.Entity<Grupa>()
            .HasOne(g => g.Kolonia)
            .WithMany()
            .HasForeignKey(g => g.KoloniaId);
    }

    private void ConfigureKolonia(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kolonia>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<Kolonia>()
            .HasOne(k => k.Adres)
            .WithMany()
            .HasForeignKey(k => k.AdresId);
        modelBuilder.Entity<Kolonia>()
            .HasOne(k => k.Firma)
            .WithMany()
            .HasForeignKey(k => k.FirmaId);
        modelBuilder.Entity<Kolonia>()
            .HasOne(k => k.Forma)
            .WithMany()
            .HasForeignKey(k => k.FormaId);
    }

    private void ConfigureKoloniaDziecko(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KoloniaDziecko>()
            .HasKey(kd => kd.Id);
        modelBuilder.Entity<KoloniaDziecko>()
            .HasOne(kd => kd.Dziecko)
            .WithMany()
            .HasForeignKey(kd => kd.DzieckoId);
        modelBuilder.Entity<KoloniaDziecko>()
            .HasOne(kd => kd.Grupa)
            .WithMany()
            .HasForeignKey(kd => kd.GrupaId);
        modelBuilder.Entity<KoloniaDziecko>()
            .HasOne(kd => kd.Status)
            .WithMany()
            .HasForeignKey(kd => kd.StatusId);
    }

    private void ConfigureKonto(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Konto>()
            .HasKey(k => k.Id);
        modelBuilder.Entity<Konto>()
            .HasOne(k => k.Upr)
            .WithMany()
            .HasForeignKey(k => k.UprId);
    }

    private void ConfigureMiasto(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Miasto>()
            .HasKey(m => m.Id);
    }

    private void ConfigureObserwacja(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Obserwacja>()
            .HasKey(o => o.Id);
        modelBuilder.Entity<Obserwacja>()
            .HasOne(o => o.Dziecko)
            .WithMany()
            .HasForeignKey(o => o.IdzieckoId);
        modelBuilder.Entity<Obserwacja>()
            .HasOne(o => o.RodzObs)
            .WithMany()
            .HasForeignKey(o => o.IrodzId);
    }

    private void ConfigureOpiekun(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Opiekun>()
            .HasKey(o => o.Id);
        modelBuilder.Entity<Opiekun>()
            .HasOne(o => o.Konto)
            .WithMany()
            .HasForeignKey(o => o.KontoId);
    }

    private void ConfigureOpiekunGrupa(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OpiekunGrupa>()
            .HasKey(og => og.Id);
        modelBuilder.Entity<OpiekunGrupa>()
            .HasOne(og => og.Grupa)
            .WithMany()
            .HasForeignKey(og => og.GrupaId);
        modelBuilder.Entity<OpiekunGrupa>()
            .HasOne(og => og.Opiekun)
            .WithMany()
            .HasForeignKey(og => og.OpiekunId);
    }

    private void ConfigurePlatnosc(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platnosc>()
            .HasOne(p => p.KoloniaDziecko)
            .WithMany()
            .HasForeignKey(p => p.KoloniaDzieckoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Platnosc>()
            .HasOne(p => p.Rodzic)
            .WithMany()
            .HasForeignKey(p => p.RodzicId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Platnosc>()
            .HasOne(p => p.Waluta)
            .WithMany()
            .HasForeignKey(p => p.WalutaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Platnosc>()
            .HasOne(p => p.RodzajPlatnosci)
            .WithMany()
            .HasForeignKey(p => p.RodzajPlatnosciId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Platnosc>()
            .HasOne(p => p.StatusPlatnosci)
            .WithMany()
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureRodzic(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rodzic>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<Rodzic>()
            .HasOne(r => r.Adres)
            .WithMany()
            .HasForeignKey(r => r.AdresId);
        modelBuilder.Entity<Rodzic>()
            .HasOne(r => r.Konto)
            .WithMany()
            .HasForeignKey(r => r.KontoId);
    }

    private void ConfigureStatus(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>()
            .HasKey(s => s.Id);
    }

    private void ConfigureUlica(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ulica>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<Ulica>()
            .HasOne(u => u.Miasto)
            .WithMany()
            .HasForeignKey(u => u.MiastoId);
    }

    private void ConfigureForma(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Forma>()
            .HasKey(f => f.Id);
    }

    private void ConfigureRodzObs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RodzObs>()
            .HasKey(ro => ro.Id);
    }

    private void ConfigureRodzajPlatnosci(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RodzajPlatnosci>()
            .HasKey(rp => rp.Id);
    }

    private void ConfigureStatusPlatnosci(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StatusPlatnosci>()
            .HasKey(sp => sp.Id);
    }

    private void ConfigureUpr(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Upr>()
            .HasKey(u => u.Id);
    }

    private void ConfigureWaluta(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Waluta>()
            .HasKey(w => w.Id);
    }
}
