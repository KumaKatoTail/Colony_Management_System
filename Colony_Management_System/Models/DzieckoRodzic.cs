namespace Colony_Management_System.Models
{
    public class DzieckoRodzic
    {
        public int Id { get; set; }
        public int DzieckoId { get; set; }
        public int RodzicId { get; set; }

        public Dziecko Dziecko { get; set; }
        public Rodzic Rodzic { get; set; }
    }
}
