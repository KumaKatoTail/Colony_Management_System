using Colony_Management_System.Models.Colony_Management_System.Models;

namespace Colony_Management_System.Models
{
    public class KoloniaDziecko
    {
        public int Id { get; set; }
        public int DzieckoId { get; set; }
        public int GrupaId { get; set; }
        public int StatusId { get; set; }
        public DateTime DataZapisu { get; set; }

        public Dziecko Dziecko { get; set; }
        public Grupa Grupa { get; set; }
        public Status Status { get; set; }
    }
}
