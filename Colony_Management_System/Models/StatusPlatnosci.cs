namespace Colony_Management_System.Models
{
    public class StatusPlatnosci
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<Platnosc> Platnosci { get; set; }
    }
}
