namespace Colony_Management_System.Models
{
    public class Ulica
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public int MiastoId { get; set; }

        public Miasto Miasto { get; set; }
        public ICollection<Adres> Adresy { get; set; }
    }
}
