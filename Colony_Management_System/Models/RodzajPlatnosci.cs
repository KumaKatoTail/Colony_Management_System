namespace Colony_Management_System.Models
{
    public class RodzajPlatnosci
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
