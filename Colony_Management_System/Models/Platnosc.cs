namespace Colony_Management_System.Models
{
    public class Platnosc
    {
        public int Id { get; set; }
        public int KoloniaDzieckoId { get; set; }
        public bool CzyFaktura { get; set; }
        public double Kwota { get; set; }
        public DateTime DataPlatnosci { get; set; }
        public string Opis { get; set; }
        public string NumerRef { get; set; }
        public int IdTranzakcja { get; set; }
        public string Bramka { get; set; }
        public double OplataPosrednia { get; set; }
        public int RodzicId { get; set; }
        public int WalutaId { get; set; }
        public int RodzajPlatnosciId { get; set; }
        public int StatusId { get; set; }

        public KoloniaDziecko KoloniaDziecko { get; set; }
        public Rodzic Rodzic { get; set; }
        public Waluta Waluta { get; set; }
        public RodzajPlatnosci RodzajPlatnosci { get; set; }
        public StatusPlatnosci StatusPlatnosci { get; set; }
    }
}
