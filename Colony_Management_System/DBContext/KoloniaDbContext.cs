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
            this._connectionString = connectionString;//"Server=sql7.freesqldatabase.com;Port=3306;Database=sql7759030;Uid=sql7759030;Pwd=wYaz8HFV7h;Charset=utf8;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Używamy Pomelo.EntityFrameworkCore.MySql do połączenia z MySQL
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacje
            modelBuilder.Entity<Administrator>()
                .HasOne(a => a.Konto)
                .WithMany()
                .HasForeignKey(a => a.KontoId);

            modelBuilder.Entity<Administrator>()
                .HasOne(a => a.Firma)
                .WithMany()
                .HasForeignKey(a => a.FirmaId);

            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Ulica)
                .WithMany()
                .HasForeignKey(a => a.UlicaId);

            modelBuilder.Entity<Dziecko>()
                .HasOne(d => d.Adres)
                .WithMany()
                .HasForeignKey(d => d.AdresId);

            modelBuilder.Entity<DzieckoRodzic>()
                .HasOne(dr => dr.Dziecko)
                .WithMany()
                .HasForeignKey(dr => dr.DzieckoId);

            modelBuilder.Entity<DzieckoRodzic>()
                .HasOne(dr => dr.Rodzic)
                .WithMany()
                .HasForeignKey(dr => dr.RodzicId);

            modelBuilder.Entity<Firma>()
                .HasOne(f => f.Adres)
                .WithMany()
                .HasForeignKey(f => f.AdresId);

            modelBuilder.Entity<Grupa>()
                .HasOne(g => g.Kolonia)
                .WithMany()
                .HasForeignKey(g => g.KoloniaId);

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

            modelBuilder.Entity<Kolonia>()
                .HasOne(k => k.Firma)
                .WithMany()
                .HasForeignKey(k => k.FirmaId);

            modelBuilder.Entity<Kolonia>()
                .HasOne(k => k.Adres)
                .WithMany()
                .HasForeignKey(k => k.AdresId);

            modelBuilder.Entity<Kolonia>()
                .HasOne(k => k.Forma)
                .WithMany()
                .HasForeignKey(k => k.FormaId);

            modelBuilder.Entity<Konto>()
                .HasOne(k => k.Upr)
                .WithMany()
                .HasForeignKey(k => k.UprId);

            modelBuilder.Entity<Obserwacja>()
                .HasOne(o => o.Dziecko)
                .WithMany()
                .HasForeignKey(o => o.DzieckoId);

            modelBuilder.Entity<Obserwacja>()
                .HasOne(o => o.RodzajObserwacji)
                .WithMany()
                .HasForeignKey(o => o.RodzajObserwacjiId);

            modelBuilder.Entity<OpiekunGrupa>()
                .HasOne(og => og.Grupa)
                .WithMany()
                .HasForeignKey(og => og.GrupaId);

            modelBuilder.Entity<OpiekunGrupa>()
                .HasOne(og => og.Opiekun)
                .WithMany()
                .HasForeignKey(og => og.OpiekunId);

            modelBuilder.Entity<Opiekun>()
                .HasOne(o => o.Konto)
                .WithMany()
                .HasForeignKey(o => o.KontoId);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.KoloniaDziecko)
                .WithMany()
                .HasForeignKey(p => p.KoloniaDzieckoId);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.Rodzic)
                .WithMany()
                .HasForeignKey(p => p.RodzicId);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.RodzajPlatnosci)
                .WithMany()
                .HasForeignKey(p => p.RodzajPlatnosciId);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.StatusPlatnosci)
                .WithMany()
                .HasForeignKey(p => p.StatusId);

            modelBuilder.Entity<Platnosc>()
                .HasOne(p => p.Waluta)
                .WithMany()
                .HasForeignKey(p => p.WalutaId);

            modelBuilder.Entity<Rodzic>()
                .HasOne(r => r.Adres)
                .WithMany()
                .HasForeignKey(r => r.AdresId);

            modelBuilder.Entity<Rodzic>()
                .HasOne(r => r.Konto)
                .WithMany()
                .HasForeignKey(r => r.KontoId);

            modelBuilder.Entity<Ulica>()
                .HasOne(u => u.Miasto)
                .WithMany()
                .HasForeignKey(u => u.MiastoId);
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
        public DbSet<RodzajObserwacji> RodzajObserwacji { get; set; }
        public DbSet<RodzajPlatnosci> RodzajPlatnosci { get; set; }
        public DbSet<StatusPlatnosci> StatusPlatnosci { get; set; }
        public DbSet<Upr> Upr { get; set; }
        public DbSet<Waluta> Waluta { get; set; }
    }
}
