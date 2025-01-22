namespace Colony_Management_System.Models
{
    namespace Colony_Management_System.Models
    {
        public class Rodzic
        {
            public int Id { get; set; }
            public int KontoId { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Telefon { get; set; }
            public string Mail { get; set; }
            public int AdresId { get; set; }

            public Konto Konto { get; set; }
            public Adres Adres { get; set; }
            public ICollection<DzieckoRodzic> DzieciRodzice { get; set; }
            public ICollection<Platnosc> Platnosci { get; set; }
        }
    }

}
