namespace Colony_Management_System.Models
{
    public class Dziecko
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        public int AdresId { get; set; }

        public Adres Adres { get; set; }
        public ICollection<DzieckoRodzic> DzieckoRodzice { get; set; }
        public ICollection<KoloniaDziecko> KolonieDziecka { get; set; }
    }
}
