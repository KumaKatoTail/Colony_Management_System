namespace Colony_Management_System.Models
{
    public class KoloniaDto
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public DateTime TerminOd { get; set; }
        public DateTime TerminDo { get; set; }
        public FirmaDto Firma { get; set; }
        public AdresDto Adres { get; set; }
        public FormaDto Forma { get; set; }
        public string TrasaWedrowna { get; set; }
        public string Kraj { get; set; }
    }
}
