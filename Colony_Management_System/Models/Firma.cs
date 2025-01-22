namespace Colony_Management_System.Models
{
    public class Firma
    {
        public int Id { get; set; }
        public int AdresId { get; set; }
        public string Nazwa { get; set; }

        public Adres Adres { get; set; }
        public ICollection<Kolonia> Kolonie { get; set; }
    }
}
