namespace Colony_Management_System.Models
{
    public class Waluta
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
