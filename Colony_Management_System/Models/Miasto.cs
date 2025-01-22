namespace Colony_Management_System.Models
{
    public class Miasto
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Kod { get; set; }

        public ICollection<Ulica> Ulice { get; set; }
    }
}
