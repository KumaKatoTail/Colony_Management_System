namespace Colony_Management_System.Models
{
    public class Obserwacja
    {
        public int Id { get; set; }
        public int IrodzId { get; set; }
        public int IdzieckoId { get; set; }
        public string Opis { get; set; }

        public RodzObs RodzObs { get; set; }
        public Dziecko Dziecko { get; set; }
    }
}
