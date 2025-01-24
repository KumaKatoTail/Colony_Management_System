using Microsoft.EntityFrameworkCore;
using Colony_Management_System.Models;

namespace Colony_Management_System.Models.DbContext
{
    public class KoloniaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        private readonly string _connectionString;

        public KoloniaDbContext(DbContextOptions<KoloniaDbContext> options)
            : base(options)
        {
        }

        public KoloniaDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(8, 3, 0)));
            }
        }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacje
            modelBuilder.Entity<Administrator>()
                .HasOne(a => a.Konto)
                .WithMany()
                .HasForeignKey(a => a.KontoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Administrator>()
                .HasOne(a => a.Firma)
                .WithMany()
                .HasForeignKey(a => a.FirmaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Ulica)
                .WithMany()
                .HasForeignKey(a => a.UlicaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dziecko>()
                .HasOne(d => d.Adres)
                .WithMany()
                .HasForeignKey(d => d.AdresId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DzieckoRodzic>()
                .HasOne(dr => dr.Dziecko)
                .WithMany()
                .HasForeignKey(dr => dr.DzieckoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DzieckoRodzic>()
                .HasOne(dr => dr.Rodzic)
                .WithMany()
                .HasForeignKey(dr => dr.RodzicId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Firma>()
                .HasOne(f => f.Adres)
                .WithMany()
                .HasForeignKey(f => f.AdresId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grupa>()
                .HasOne(g => g.Kolonia)
                .WithMany()
                .HasForeignKey(g => g.KoloniaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KoloniaDziecko>()
                .HasOne(kd => kd.Dziecko)
                .WithMany()
                .HasForeignKey(kd => kd.DzieckoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KoloniaDziecko>()
                .HasOne(kd => kd.Grupa)
                .WithMany()
                .HasForeignKey(kd => kd.GrupaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KoloniaDziecko>()
                .HasOne(kd => kd.Status)
                .WithMany()
                .HasForeignKey(kd => kd.StatusId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Kolonia>()
                .HasOne(k => k.Firma)
                .WithMany()
                .HasForeignKey(k => k.FirmaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Kolonia>()
                .HasOne(k => k.Adres)
                .WithMany()
                .HasForeignKey(k => k.AdresId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Kolonia>()
                .HasOne(k => k.Forma)
                .WithMany()
                .HasForeignKey(k => k.FormaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Konto>()
                .HasOne(k => k.Upr)
                .WithMany()
                .HasForeignKey(k => k.UprId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Obserwacja>()
                .HasOne(o => o.Dziecko)
                .WithMany()
                .HasForeignKey(o => o.IdzieckoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Obserwacja>()
                .HasOne(o => o.RodzObs)
                .WithMany()
                .HasForeignKey(o => o.IrodzId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OpiekunGrupa>()
                .HasOne(og => og.Grupa)
                .WithMany()
                .HasForeignKey(og => og.GrupaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OpiekunGrupa>()
                .HasOne(og => og.Opiekun)
                .WithMany()
                .HasForeignKey(og => og.OpiekunId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Opiekun>()
                .HasOne(o => o.Konto)
                .WithMany()
                .HasForeignKey(o => o.KontoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.KoloniaDziecko)
                .WithMany()
                .HasForeignKey(p => p.KoloniaDieckoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.Rodzic)
                .WithMany()
                .HasForeignKey(p => p.RodzicId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.RodzajPlatnosci)
                .WithMany()
                .HasForeignKey(p => p.RodzajPlatnosciId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.StatusPlatnosci)
                .WithMany()
                .HasForeignKey(p => p.StatusId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.Waluta)
                .WithMany()
                .HasForeignKey(p => p.WalutaId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rodzic>()
                .HasOne(r => r.Adres)
                .WithMany()
                .HasForeignKey(r => r.AdresId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rodzic>()
                .HasOne(r => r.Konto)
                .WithMany()
                .HasForeignKey(r => r.KontoId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ulica>()
                .HasOne(u => u.Miasto)
                .WithMany()
                .HasForeignKey(u => u.MiastoId).OnDelete(DeleteBehavior.Cascade);

            // Dodatkowe konfiguracje, jeśli są wymagane
        }

        // DbSety dla wszystkich tabel
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
    }
}
