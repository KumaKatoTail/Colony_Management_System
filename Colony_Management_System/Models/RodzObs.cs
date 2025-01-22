namespace Colony_Management_System.Models
{
    public class RodzObs
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Obserwacja> Obserwacje { get; set; }
    }
}
