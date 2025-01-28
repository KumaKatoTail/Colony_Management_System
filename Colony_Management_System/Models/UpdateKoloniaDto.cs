namespace Colony_Management_System.Models
{
    public class UpdateKoloniaDto
    {
        public string? Nazwa { get; set; }
        public string? TrasaWedrowna { get; set; }
        public string? Opis { get; set; }
        public string? Kraj { get; set; }
        public DateTime? TerminOd { get; set; }
        public DateTime? TerminDo { get; set; }
    }

}
