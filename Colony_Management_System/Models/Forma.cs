using Colony_Management_System.Models.Colony_Management_System.Models;

namespace Colony_Management_System.Models
{
    public class Forma
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Kolonia> Kolonie { get; set; }
    }
}
